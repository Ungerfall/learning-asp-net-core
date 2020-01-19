# IO.Swagger.Api.CategoryApi

All URIs are relative to *https://localhost:44339*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CategoryDeleteCategory**](CategoryApi.md#categorydeletecategory) | **DELETE** /api/Category/{id} | 
[**CategoryGetCategories**](CategoryApi.md#categorygetcategories) | **GET** /api/Category | 
[**CategoryGetCategory**](CategoryApi.md#categorygetcategory) | **GET** /api/Category/{id}.{format} | 
[**CategoryPostCategory**](CategoryApi.md#categorypostcategory) | **POST** /api/Category | 
[**CategoryPutCategory**](CategoryApi.md#categoryputcategory) | **PUT** /api/Category/{id} | 


<a name="categorydeletecategory"></a>
# **CategoryDeleteCategory**
> Categories CategoryDeleteCategory (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryDeleteCategoryExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();
            var id = 56;  // int? | 

            try
            {
                Categories result = apiInstance.CategoryDeleteCategory(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryDeleteCategory: " + e.Message );
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

[**Categories**](Categories.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="categorygetcategories"></a>
# **CategoryGetCategories**
> List<Categories> CategoryGetCategories ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryGetCategoriesExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();

            try
            {
                List&lt;Categories&gt; result = apiInstance.CategoryGetCategories();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryGetCategories: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<Categories>**](Categories.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="categorygetcategory"></a>
# **CategoryGetCategory**
> Categories CategoryGetCategory (int? id, string format)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryGetCategoryExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();
            var id = 56;  // int? | 
            var format = format_example;  // string | 

            try
            {
                Categories result = apiInstance.CategoryGetCategory(id, format);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryGetCategory: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 
 **format** | **string**|  | 

### Return type

[**Categories**](Categories.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="categorypostcategory"></a>
# **CategoryPostCategory**
> Categories CategoryPostCategory (Categories category)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryPostCategoryExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();
            var category = new Categories(); // Categories | 

            try
            {
                Categories result = apiInstance.CategoryPostCategory(category);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryPostCategory: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **category** | [**Categories**](Categories.md)|  | 

### Return type

[**Categories**](Categories.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="categoryputcategory"></a>
# **CategoryPutCategory**
> System.IO.Stream CategoryPutCategory (int? id, Categories category)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryPutCategoryExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();
            var id = 56;  // int? | 
            var category = new Categories(); // Categories | 

            try
            {
                System.IO.Stream result = apiInstance.CategoryPutCategory(id, category);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryPutCategory: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 
 **category** | [**Categories**](Categories.md)|  | 

### Return type

**System.IO.Stream**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

