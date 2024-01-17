## User Controller

### Sign Up
**URL:** `/user/sign-up`

**Method:** `POST`

**Request Example:**
```json
{
  "email": "example@email.com",
  "password": "mypassword123",
  "name": "Joe",
  "phoneNumber": "+11111111",
  "address": {
    "street": "Main St.",
    "zipCode": "10001",
    "city": "New York",
    "country": "USA"
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
  "email": "example@email.com",
  "password": "mypassword123"
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
  "newName": "Joe",
  "newPhoneNumber": "+111111111",
  "titleofAddressToUpdate": null,
  "userAddress": {
    "title": "Work",
    "address": {
      "street": "Main St.",
      "zipCode": "10001",
      "city": "New York",
      "country": "USA"
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
  "currentPassword": "mypassword123",
  "newEmail": "example@email.com",
  "newPassword": null
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
