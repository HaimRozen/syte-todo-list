# syte-todo-list
TodoList service implemented using .Net Core 5.0 API with MongoDB database.
Use the following instruction in order to run the service:

1. Run MongoDB Docker locally:
1.1 From your shell run:
docker pull mongo
docker run -d  --name mongo-on-docker  -p 27888:27017 -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=secret mongo

1.2 Verify that MongoDB is running using: docker ps -a

2. Run .net
