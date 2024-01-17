## Order Controller

### Get Orders
**URL:** `/order/get`

**Method:** `GET`

**Query Parameters:**
- `page` (int)
- `pageSize` (int)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": null,
  "payload": {
    "orders": [
      {
        "userName": "Joe",
        "deliveryAddress": {
          "street": "Main St.",
          "zipCode": "10001",
          "city": "New York",
          "country": "USA"
        },
        "createdAt": "2024-01-15T14:37:04.820892Z",
        "items": [
          {
            "productName": "Product 1",
            "unitPrice": {
              "currencyCode": "USD",
              "amount": 5.99
            },
            "quantity": 1
          }
        ]
      }
    ]
  },
  "metaData": null
}
```

---

### Get My Orders
**URL:** `/order/my-orders`

**Method:** `GET`

**Query Parameters:**
- `page` (int)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": null,
  "payload": {
    "orders": [
      {
        "userName": "Joe",
        "deliveryAddress": {
          "street": "Main St.",
          "zipCode": "10001",
          "city": "New York",
          "country": "USA"
        },
        "createdAt": "2024-01-15T14:37:04.820892Z",
        "items": [
          {
            "productName": "Product 1",
            "unitPrice": {
              "currencyCode": "USD",
              "amount": 5.99
            },
            "quantity": 1
          }
        ]
      }
    ]
  },
  "metaData": null
}
```

---

### Add Message To Ticket
**URL:** `/order/{orderId}/ticket/add`

**Method:** `POST`

**Route Parameters:**
- `orderId` (Guid)

**Request Example:**
```json
{
  "content": "Message content"
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The message has been sent",
  "metaData": null
}
```

---

### Close Ticket
**URL:** `/order/{orderId}/ticket/close`

**Method:** `PUT`

**Route Parameters:**
- `orderId` (Guid)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The ticket has been closed",
  "metaData": null
}
```
