# IO.Swagger.Api.ProductApi

All URIs are relative to *https://localhost:44339*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ProductDeleteProduct**](ProductApi.md#productdeleteproduct) | **DELETE** /api/Product/{id} | 
[**ProductGetProduct**](ProductApi.md#productgetproduct) | **GET** /api/Product/{id} | 
[**ProductGetProducts**](ProductApi.md#productgetproducts) | **GET** /api/Product | 
[**ProductPostProduct**](ProductApi.md#productpostproduct) | **POST** /api/Product | 
[**ProductPutProduct**](ProductApi.md#productputproduct) | **PUT** /api/Product/{id} | 


<a name="productdeleteproduct"></a>
# **ProductDeleteProduct**
> Products ProductDeleteProduct (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ProductDeleteProductExample
    {
        public void main()
        {
            var apiInstance = new ProductApi();
            var id = 56;  // int? | 

            try
            {
                Products result = apiInstance.ProductDeleteProduct(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductApi.ProductDeleteProduct: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 

### Return type

[**Products**](Products.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="productgetproduct"></a>
# **ProductGetProduct**
> Products ProductGetProduct (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ProductGetProductExample
    {
        public void main()
        {
            var apiInstance = new ProductApi();
            var id = 56;  // int? | 

            try
            {
                Products result = apiInstance.ProductGetProduct(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductApi.ProductGetProduct: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 

### Return type

[**Products**](Products.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="productgetproducts"></a>
# **ProductGetProducts**
> List<Products> ProductGetProducts ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ProductGetProductsExample
    {
        public void main()
        {
            var apiInstance = new ProductApi();

            try
            {
                List&lt;Products&gt; result = apiInstance.ProductGetProducts();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductApi.ProductGetProducts: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<Products>**](Products.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="productpostproduct"></a>
# **ProductPostProduct**
> Products ProductPostProduct (Products product)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ProductPostProductExample
    {
        public void main()
        {
            var apiInstance = new ProductApi();
            var product = new Products(); // Products | 

            try
            {
                Products result = apiInstance.ProductPostProduct(product);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductApi.ProductPostProduct: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **product** | [**Products**](Products.md)|  | 

### Return type

[**Products**](Products.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="productputproduct"></a>
# **ProductPutProduct**
> System.IO.Stream ProductPutProduct (int? id, Products product)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ProductPutProductExample
    {
        public void main()
        {
            var apiInstance = new ProductApi();
            var id = 56;  // int? | 
            var product = new Products(); // Products | 

            try
            {
                System.IO.Stream result = apiInstance.ProductPutProduct(id, product);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductApi.ProductPutProduct: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 
 **product** | [**Products**](Products.md)|  | 

### Return type

**System.IO.Stream**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

