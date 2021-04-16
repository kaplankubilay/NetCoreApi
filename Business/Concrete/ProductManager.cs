using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Business.Abstract;
using Business.BusinessAspect.Autofact;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Performance;
using Core.Aspect.Autofac.Transaction;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Business;
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

        /// <summary>
        /// "PerformanceAspect" eğer 5 saniyeyi geçerse hata vermeyecek ancak output a yazacaktır.
        /// </summary>
        /// <returns></returns>
        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetList()
        {
            //işlemi 5 sn bekleterek output a gecikme olduğu bilgisi yazdırıdı. 
            Thread.Sleep(5000);

            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        /// <summary>
        /// Veri bir dk boyunca cache ten gelecek sonra veri, veritabanından gelecek. Ardından çekilen veri yine cache e eklenecek.
        /// Ve cache bu yolla dakikada bir güncellenerek çekilen veri beslenmiş olacak. (default değeri 60 dk olarak tanımlandı)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [SecuredOperation("Admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        /// <summary>
        /// CacheRemoveAspect= "IProductService altında bulunan ve adında "Get" key i barındıran metodlara bağlı cache bilgilerini silecek..
        /// "Priority" ile birden fazla attribute arasından hangi öncelikte çalışacagı belirtilir.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //"CheckIfProductExists" metodunun arkasına başka kontrol metodları da eklenerek "Success-Error değeri toplu olarak döndürülebilir."
            IResult result = BusinessRules.Run(CheckIfProductExists(product.ProductName));

            if (result!=null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        private IResult CheckIfProductExists(string productName)
        {
            var result = _productDal.GetList(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(result, Messages.ProductNameAlreadyExist);
            }

            return new SuccessResult();
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
