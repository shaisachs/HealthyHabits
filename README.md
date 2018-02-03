# HealthyHabits

An API to keep track of healthy habits on a daily basis. Read more about it on [my blog](https://shaisachs.github.io/2018/02/03/healthyhabits-fleshing-out-api-stack.html?src=github)!

## APIs

Following is a synopsis; for full details, check out the [Swagger doc](http://healthyhabitsapi.azurewebsites.net/swagger/)

Creating a habit:

```
POST /api/v1/habits
{
  "name": "Exercise"
}
```

Marking a habit complete for a particular day:

```
POST /api/v1/habits/123/completions
{
  "completed": "2018-01-01"
}
```

Full CRUD operations are available on both routes. Complete API documentation available at /swagger.

Logo from [Sanja on openclipart](https://openclipart.org/detail/183893/simple-red-apple)
