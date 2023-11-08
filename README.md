# Rabbitmq pubsub
Simple domonstration of how to use rabbitMQ for publish and subscribe events or messages.

Setup RabbitMQ on docker as follows.


### RabbitMQ on docker
Using RabbmitMQ management so that we can get the RabbitMQ UI in localhost:15672

```docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management```

### Run the projects
There are two projects in this solution. Run the subscriber project first and then run the publisher.

Enjoy!
