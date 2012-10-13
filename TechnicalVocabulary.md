# Technical Vocabulary

This document provides definitions to core terminology used throughout this project. 

It will be grouped into 3 sections:

* Domain-Driven Design
* Lokad.CQRS Terminology
* Terminology of "Don't Break The Chain"



## 1. Domain Driven Design

A collection of guidelines and modeling tools with focus on analyzing and modeling the core of business problem and related processes. Beware, you will have to understand domain you're working with in depth, which usually requires a lot of time (often due to a large number of details that are not apparent initially).

We use DDD to identify problems that are worth solving in the problem space and provide solutions for them.

#### Domain

Our current understanding of the business concepts, problems and solutions, as captured in models, currently implemented processes and software.

Existing solution might be suboptimal and not the most efficient (e.g.: usage of a single ERP for everything in a huge automotive design).

#### Subdomains


Entire domain can usually be split into smaller relatively separate elements, to prioritise their importance and identify some common patterns:

* **Core Subdomain** - the most important part, essential to core business focus of the company. This is where company derives the most value and it's competitive advantage (that's how it makes money).
* **Support Subomain** - grouped aspects of a business, specific to the business of an organization, but not essential for it to succeed. It is important, but much less important than Core Domain.
* **Generic Subdomain** - aspects of business that are neither specific to it nor critical for its success.


#### Bounded Context (BC)

Bounded Context is a natural boundary around some aspects of a business in real world. We can use certain methods to identify these boundaries and break down complex problem space into a set of somewhat separate BCs, which can sometimes be tackled one by one.

Bounded contexts can be identified in organization by things like:

* looking for groups of people that use the same language;
* business processes that are closely related;
* documents that are usually worked with together;
* department structure;
* structure of teams that work together on certain solutions.

#### Context Map

In ideal world, there is 1-to-1 correlation between problem boundaries (Bounded Contexts) and solutions that were created to deal with them (Subdomains). Yet, in reality the situation can be less clean. 

We can have legacy solutions in place that attempt to solve too many problems at once, problems that remain completely unsolved or simply a confusing big ball of mud.

In order to move forward in such environment we start by creating a map of the current situations - **context map**. It is our current understanding (which can be imperfect) about current business problems and solutions to them (which are almost always imperfect).

Context map helps us to understand current situation and also prioritise our actions in providing most efficient solutions to the problems that are worth solving.


## 2. CQRS

#### Command Query Responsibility Segregation (CQRS)

Software design pattern in which code reading data from storage is consciously kept separate from the code which writes data to a storage. In many systems, especially as they become more complex, writing data and reading data require different approaches (both for optimization and to keep code simple). Keeping them separate allows each piece of code to focus on the read or write specific needs. The term was first coined by Greg Young and is based off CQS (Command Query Separation). Watch http://www.youtube.com/watch?v=KXqrBySgX-s for more details.

#### Event Sourcing

An idea of capturing everything that happens in the system in a stream of events. Thus no information is ever lost. When current (or not) state of the system is needed, events are replayed in the order they happened in the past until information we're interested in is generated.

#### Message

Data structure with a distinct name and some associated information, which can be persisted or sent over the network. An email or letter are examples of messages from the real world.

Learn more: [BTW Episode 2 - Messaging Basics](http://beingtheworst.com/2012/episode-2-messaging-basics)


#### Envelope

Wrapper around message which helps to send it between systems, provides facilities to identify the message, prevent (or at least check for) corruption, etc.

#### Command Message (Command)

Command is just a type of the message with which we associate specific meaning and behaviour. Command message is usually an instruction (request) for server or another recipient to perform a certain action. Based on the situation (e.g. availability of resources) recipient would be able to perform this action or arrive at some other outcome. We can have certain expectations about such outcome, but we are never sure.


    public class AddChainLink : ICommand
    {
        public ChainId Id { get; set; }
        public DateTime LinkDate { get; set; }
        public string Comment { get; set; }
    }   
    
    
Command messages are usually sent either to a specific recipient or location (message queue).

#### Event Message (Event)

Event is a message that tells us about something that already happened in the past.


    public class ChainLinkAdded : IEvent
    {
        public ChainId Id { get; set; }
        public DateTime LinkDate { get; set; }
        public string Comment { get; set; }        
        public DateTime AddedOn { get; set; }
    }       

In many cases events are generated in response to commands, when they're processed. In some circumstances event can come from outside the system notifying the system what happened in the outside world. Events are immutable. Once they're published they cannot be modified. If there's a problem a compensating command needs to be issued to fix the problem.

#### Message Handler

Code that is being executed by recipient to process incoming message.

    public void When(AddChainLink c)
    {
        // do something with DB
    }
    
**Command Handler** is name for method handling command messages. Likewise, **Event Handler** method handles events.

#### Message Queue

Queue is like email inbox or queue at the store. It is used as a temporary storage location for messages before they can be processed by the recipient. Quite often queues are also used for communication between different parts of a distributed system (sender knows where to put messages and recipient knows where to pick them up).

Different types of message queues can have different guarantees with regards to ordering (order is preserved or not) and reliability (message is delivered at most once, at least once or exactly once).

#### Event Storage

Specialized component or a subsystem dedicated to recording history of all relevant messages in the system. It frequently also manages replication, backups and failover.

Messages being recorded in this store are usually associated with key. Messages that have same key in an event store are known to be in the same event stream. 

Event storage can have any number of event streams.

#### View

View is a read model which is derived from some other data and optimized for querying (as in CQRS). In event-centric architectures views are usually generated and kept up-to-date by projecting events.

#### Projection

A set of rules on how to process events to generate a view. It is usually represented by a set of event handlers grouped within one class.



## 3. Don't Break The Chain

> TBD