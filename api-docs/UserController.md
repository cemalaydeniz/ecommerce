## User Controller

### Sign Up
**URL:** `/user/sign-up`

**Method:** `POST`

**Request Example:**
```json
{
  "email": "string",
  "password": "string",
  "name": "string",
  "phoneNumber": "string",
  "address": {
    "street": "string",
    "zipCode": "string",
    "city": "string",
    "country": "string"
  }
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 201,
  "message": "You have signed up successfully",
  "metaData": null
}
```

---

### Sign In
**URL:** `/user/sign-in`

**Method:** `PUT`

**Request Example:**
```json
{
  "email": "string",
  "password": "string"
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "You have signed in successfully",
  "payload": {
    "accessToken": "eyJhb...ipXBW-uc36zc"
  },
  "metaData": null
}
```

---

### Sign Out
**URL:** `/user/sign-in`

**Method:** `PUT`

**Query Parameters:**
- `signOutAllDevices` (boolean)
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "You have signed out successfully",
  "metaData": null
}
```

---

### User Profile
**URL:** `/user/profile`

**Method:** `GET`

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": null,
  "payload": {
    "email": "example@email.com",
    "name": "Joe",
    "phoneNumber": "+111111111",
    "addresses": [
      {
        "title": "Home",
        "address": {
          "street": "Main St.",
          "zipCode": "10001",
          "city": "New York",
          "country": "USA"
        }
      }
    ]
  },
  "metaData": null
}
```

---

### Update Profile
**URL:** `/user/profile/update-profile`

**Method:** `PUT`

**Request Example:**
```json
{
  "newName": "string",
  "newPhoneNumber": "string",
  "titleofAddressToUpdate": "string",
  "userAddress": {
    "title": "string",
    "address": {
      "street": "string",
      "zipCode": "string",
      "city": "string",
      "country": "string"
    }
  }
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The profile has been updated",
  "metaData": null
}
```

---

### Update Credentials
**URL:** `/user/profile/update-credentials`

**Method:** `PUT`

**Request Example:**
```json
{
  "currentPassword": "string",
  "newEmail": "string",
  "newPassword": "string"
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The credentials has been updated",
  "metaData": null
}
```

---

### Soft Delete User
**URL:** `/user/soft-delete/{userId}`

**Method:** `PUT`

**Route Parameters:**
- `userId` (Guid)
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The user has been soft deleted",
  "metaData": null
}
```
