# syte-todo-list
TodoList service - using .Net Core 5.0 API with MongoDB database (run indside docker).
Use the following instruction in order to run the service:

1. Prerequisite: docker
3. Download git repo
4. run docker-compose --> docker compose up 
5. Open TodoList API swagger using browser: http://localhost/swagger/index.html
6. Use swagger getting all endpoints

TODOList API description (you can see it from swagger):
1. GET: <url>/todo 
  Get all todo list items
  Param: No input parameters
  CURL example: curl -X GET "http://localhost/todo" -H  "accept: */*"
  Response:
  [
    {
      "id": "171f7e9a-f96b-417b-b2f4-e8d1037d26e5",
      "description": "tq",
      "creationDate": "2022-03-04T13:40:06.11Z",
      "status": 10,
      "statusDescription": "Pending"
    }
  ]
  
2. POST: <url>/todo
  Create todo list item
  Params: Payload: { "description": "Task Description"}
  CURL example: curl -X POST "http://localhost/todo" -H  "accept: */*" -H  "Content-Type: application/json" -d "{\"description\":\"tq\"}"
  Response:
  {
    "id": "171f7e9a-f96b-417b-b2f4-e8d1037d26e5",
    "description": "tq",
    "creationDate": "2022-03-04T13:40:06.1105714Z",
    "statusDescription": "Pending",
    "statusCode": 10
  }
  
3. DELETEL <url>/todo
  Delete all Todo items
  Params: no params
  CURL example: curl -X DELETE "http://localhost/todo" -H  "accept: */*"
  Response: returns number of deleted items in case of 200
  
4. GET: <url>/todo/filter/{query}
  Retrieve all todo list by query item description (case sensitive)
  CURL example: curl -X GET "http://localhost/todo/filter/Task1" -H  "accept: */*"
  Response: 
  [
    {
      "id": "6dd5053d-6595-46cf-b340-d7ac038209bc",
      "description": "tq",
      "creationDate": "2022-03-05T07:46:44.213Z",
      "status": 10,
      "statusDescription": "Pending"
    }
  ]

  5. GET: <url>/todo/{id}
  Retrieve todo list item by id
  CURL example: curl -X GET "http://localhost/todo/6dd5053d-6595-46cf-b340-d7ac038209bc" -H  "accept: */*"
  Response:
  {
    "id": "6dd5053d-6595-46cf-b340-d7ac038209bc",
    "description": "tq",
    "creationDate": "2022-03-05T07:46:44.213Z",
    "status": 10,
    "statusDescription": "Pending"
  }
  
  6. DELETE <url>/todo/{id}
  Delete specific todo item by id
  CURL example: curl -X DELETE "http://localhost/todo/6dd5053d-6595-46cf-b340-d7ac038209bc" -H  "accept: */*"
  
  7. PATCH: <url>/todo/{id}/status
  Upate todo item status
  CURL example: curl -X PATCH "http://localhost/todo/f3134a87-d415-45ca-ac1e-193700787d44/status?status=20" -H  "accept: */*"
  Status options:
  10 - pending
  20 - InProgress
  30 - Done
** we can change the API to accept string instead integer for better exprience   
