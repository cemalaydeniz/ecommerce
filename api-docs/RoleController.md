## Role Controller

### Create Role
**URL:** `/admin/roles/create`

**Method:** `POST`

**Request Example:**
```json
{
  "name": "User"
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 201,
  "message": "The role has been created",
  "metaData": null
}
```

---

### Get Role
**URL:** `/admin/roles/get/{roleId}`

**Method:** `GET`

**Route Parameters:**
- `roleId` (Guid)
  
**Query Parameters:**
- `getUsers` (boolean)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": null,
  "payload": {
    "name": "User",
    "users": [
      {
        "email": "example@email.com",
        "name": "Joe"
      }
    ]
  },
  "metaData": null
}
```

---

### Update Role
**URL:** `/admin/roles/update/{roleId}`

**Method:** `PUT`

**Route Parameters:**
- `roleId` (Guid)

**Request Example:**
```json
{
  "newName": "User"
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The role has been updated",
  "metaData": null
}
```

---

### Assign Role To User
**URL:** `/admin/roles/assign-to-user/{roleId}`

**Method:** `POST`

**Route Parameters:**
- `roleId` (Guid)

**Request Example:**
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The role has been assigned to the user",
  "metaData": null
}
```

---

### Remove Role From User
**URL:** `/admin/roles/remove-from-user/{roleId}`

**Method:** `DELETE`

**Route Parameters:**
- `roleId` (Guid)

**Request Example:**
```json
{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The role has been removed from the user",
  "metaData": null
}
```

---

### Remove Role From All Users
**URL:** `/admin/roles/remove-from-all-users/{roleId}`

**Method:** `DELETE`

**Route Parameters:**
- `roleId` (Guid)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The role has been removed from the all users",
  "metaData": null
}
```

---

### Delete Role
**URL:** `/admin/roles/delete/{roleId}`

**Method:** `DELETE`

**Route Parameters:**
- `roleId` (Guid)

**Response Example:**
```json
{
  "success": true,
  "code": 200,
  "message": "The role has been deleted",
  "metaData": null
}
```
