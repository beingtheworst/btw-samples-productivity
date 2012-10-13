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
Information in the form which allows it to be transferred from one location to another. A letter would be an example of a message in the physical world.

#### Envelope
Wrapper around message which provides facilities to identify the message, prevent (or at least check for) corruption, etc.

#### Command
Message with request to perform some operation by the system. In synchronous environment command can be rejected by the system (for example if it's invalid). In asynchronous environment command is always accepted, and if there's an issue with command a special event can be issued in response.

#### Event
Message with historical information about what happened. In many cases events are generated in response to commands, when they're processed. In some circumstances event can come from outside the system notifying the system what happened in the outside world. Events are immutable. Once they're published they cannot be modified. If there's a problem a compensating command needs to be issued to fix the problem.

#### Message Queue
Robust first in - first out stream of commands and events used for communication between different parts of the system. Usually guarantees at least once message delivery (so some form of deduplication needs to take place).

#### Event Stream (aka Tape)

An append only stream of messages. Used to record events to be replayed at a later date.

#### View
State of the system generated by replaying events.

#### Projection
A set of rules on how to process events to generate a view.