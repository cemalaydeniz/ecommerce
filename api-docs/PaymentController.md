## Payment Controller

### Initiate Payment
**URL:** `/payment`

**Method:** `POST`

**Request Example:**
```json
{
  "addressTitle": "Home",
  "currencyCode": "USD",
  "items": [
    {
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 1
    }
  ]
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": null,
  "payload": {
    "clientSecret": "pi_3Mt...luoGH",
  },
  "metaData": null
}
```
