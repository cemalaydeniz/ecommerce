## Standardized Response

### Success

**Example:**

```json
{
  "success": true,
  "code": 201,
  "message": "A new user has been created",
  "metaData": {
    "createdAt": "2024-01-15 14:37:04.820892+00"
  }
}
```

### Payload

**Example:**

```json
{
  "success": true,
  "code": 200,
  "message": "Product search is successful",
  "payload": {
    "products": [
      "name": "Product 1",
      "prices": [
        {
          "currencyCode": "USD",
          "amount": 5.99
        }
      ],
      "description": "Lorem ipsum"
    ]
  }
  "metaData": {
    "numofProducts": 1
  }
}
```

### Fail

**Example:**

```json
{
  "success": false,
  "code": 400,
  "error": [
    "The password is wrong"
  ]
  "metaData": {
    "failedAttempts": 1
  }
}
```
