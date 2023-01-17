using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchoolECommerce.Shared.Dtos
{
    public class ResponseDto<T>  //Api dönüş mesajlarıını özelleştiriyoruz
    {
        public T Data { get; set; }   //apiden gelen yanıttaki veriler (category name...)

        public int StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessful = true
            };
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccessful = true
            };
        }

        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T>
            {
                Errors = new List<string>() { error },
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
    }
}
