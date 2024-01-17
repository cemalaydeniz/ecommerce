## Product Controller

### Create Product
**URL:** `/product/create`

**Method:** `POST`

**Request Example:**
```json
{
  "name": "Product 1",
  "description": "Lorem ipsum",
  "prices": [
    {
      "currencyCode": "USD",
      "amount": 5.99
    }
  ]
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 201,
  "message": "The product has been created",
  "metaData": null
}
```

---

### Get Product
**URL:** `/product/get/{productId}`

**Method:** `GET`

**Route Parameters:**
- `productId` (Guid)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": null,
  "payload": {
    "name": "Product 1",
    "description": "Lorem ipsum",
    "prices": [
      {
        "currencyCode": "USD",
        "amount": 5.99
      }
    ]
  },
  "metaData": null
}
```

---

### Search Product
**URL:** `/product/search`

**Method:** `GET`

**Query Parameters:**
- `name` (string)
- `page` (int)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": null,
  "payload": {
    "result": [
      {
        "name": "Product 1",
        "description": "Lorem ipsum",
        "prices": [
          {
            "currencyCode": "USD",
            "amount": 5.99
          }
        ]
      }
    ]
  },
  "metaData": null
}
```

---

### Update Product
**URL:** `/product/update/{productId}`

**Method:** `PUT`

**Route Parameters:**
- `productId` (Guid)

**Request Example:**
```json
{
  "newName": "Product 1",
  "newDescription": "Lorem ipsum",
  "currencyCodesToRemove": [
    "GBP"
  ],
  "pricesToUpdateOrAdd": [
    {
      "currencyCode": "USD",
      "amount": 5.99
    }
  ]
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 201,
  "message": "The product has been updated",
  "metaData": null
}
```

---

### Make Product Free of Charge
**URL:** `/product/update/make-free/{productId}`

**Method:** `PUT`

**Route Parameters:**
- `productId` (Guid)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The product has been made free of charge",
  "metaData": null
}
```

---

### Soft Delete Product
**URL:** `/product/soft-delete/{productId}`

**Method:** `PUT`

**Route Parameters:**
- `productId` (Guid)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The product has been soft deleted",
  "metaData": null
}
```
