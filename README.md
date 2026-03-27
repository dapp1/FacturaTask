## Custom DI Container like Zenject
A custom container was used for dependency injection. Container is similar to Zenject, but much simpler and less feature-rich

## Factory
To spawn objects, a factory with automatic dependency injection from the container is used

## Object Pool
In order not to postpone optimization until “later”, a pool of objects is used.

## State Machine
Enemies have two states - Idle and Run; the state machine is used to change states. In the future, if you need to scale the project, you can easily add new states to enemies

## Event Bus
