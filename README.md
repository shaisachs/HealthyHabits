# HealthyHabits

An API to keep track of healthy habits on a daily basis.

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
