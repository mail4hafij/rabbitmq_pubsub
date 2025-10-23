# Rabbitmq pubsub
Simple domonstration of how to use rabbitMQ for publish and subscribe events or messages.
🧩 Why RabbitMQ?

- Decouples producers and consumers
- Handles message bursts with queue buffering
- Provides configurable delivery guarantees
- Supports multiple consumers via exchange types (direct, topic, fanout)

🔁 Message Delivery Modes
RabbitMQ supports different message delivery guarantees:
1. At most once	Messages may be lost but never redelivered.	High-frequency, non-critical telemetry (used here).
2. At least once	Messages are retried until acknowledged.	Reliable event delivery (e.g., billing, transactions).
3. Exactly once	Each message processed once (usually simulated via idempotency).	Rare; costly to implement.


Setup RabbitMQ on docker as follows.

### RabbitMQ on docker
Using RabbmitMQ management so that we can get the RabbitMQ UI in localhost:15672

```docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management```

### Run the projects
There are two projects in this solution. Run the subscriber project first and then run the publisher.

Enjoy!
