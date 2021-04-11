using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Transaction;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }


        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        /// <summary>
        /// Veri bir dk boyunca cache ten gelecek sonra veri, veritabanından gelecek. Ardından çekilen veri yine cache e eklenecek.
        /// Ve cache bu yolla dakikada bir güncellenerek çekilen veri beslenmiş olacak. (default değeri 60 dk olarak tanımlandı)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        //[SecuredOperation("Admin,Person")]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        /// <summary>
        /// CacheRemoveAspect= "IProductService altında bulunan ve adında "Get" key i barındıran metodlara bağlı cache bilgilerini silecek.
        /// "Priority" ile birden fazla attribute arasından hangi öncelikte çalışacagı belirtilir.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new Result(true, Messages.ProductDeleted);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new Result( true,Messages.ProductUpdated);
        }

        [TransactionScopeAspect]
        public IResult TransectionOperation(Product product)
        {
            _productDal.Update(product);
            //_productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
