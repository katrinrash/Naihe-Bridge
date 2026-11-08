# Naihe Bridge

It is a TPP cooking simulator with dark comedy elements, set in a restaurant between Heaven and Hell, where spirits come for their final meal. Players take on the role of a chef, preparing complex dishes based on customer requests while keeping up with the changing pace and increasing intensity of each shift.

> **Status:** Active Development  
> **Platform:** Windows PC

## Used Technologies:
- Unity / C#
- UI Toolkit / DOTween
- GitHub
- Jira / Mural
- FMOD

## Development Process:
"Naihe Bridge" is an ongoing project that has been in active development for approximately a year.  I've been working on the project from the very beginning as the team leader and the sole programmer. As new members joined and the team grew to five people, I transitioned into lead programmer and technical lead roles, combining programming with technical coordination and decision-making.   I'm responsible for designing and implementing the majority of the game's core gameplay systems, while also overseeing its technical direction, architecture, code quality, and development workflows to ensure maintainability, consistency, and smooth integration of new features.

## My Contributions:

- Designed and implemented the core gameplay architecture around event-driven communication and data-driven configuration using Scriptable Objects, keeping gameplay systems modular, independent, and loosely coupled.

- Developed the customer (NPC) system, including AI behaviour and navigation using Unity NavMesh:
  - Implemented procedural customer spawning and configuration based on Scriptable Object data.
  - Designed event-driven interactions between customers and other gameplay systems, avoiding direct dependencies and keeping NPC logic independent.

- Developed customer order and preference systems using data-driven configurations and event-based communication, including cooking result validation against customer requests.
- Designed and implemented a data-driven shift management system, with individual shifts and their stages fully configurable through Scriptable Objects.
- Implemented a state machine to manage gameplay progression, shift stages, and individual mini-game states and steps.

- Designed a modular mini-game architecture supporting multiple cooking activities, with reusable gameplay logic shared across different mini-games:
  - Implemented an interaction highlighting system using object outlines to indicate interactable objects and guide players through mini-game activities.

- Created custom UI Toolkit inspectors for Scriptable Objects, simplifying game configuration, content management, and gameplay balancing for a designer.

- Designed and implemented a dialogue system for special guest interactions, as well as the initial one-day gameplay loop, including shift progression, result evaluation, and results presentation.

- Developed custom gameplay animations using DOTween and C#, and integrated animations and graphical assets created by other team members.

- Implemented particle effects for mini-games and integrated audio using FMOD, building an event-driven audio system.

- Developed the main menu logic, UI flow, scene transitions, and initial scene setup.

- Focused on code quality through continuous refactoring, optimization, and architectural improvements, ensuring maintainability, extensibility, and consistency across gameplay systems.

- As Lead Programmer and Technical Lead:
  - Oversee the project's technical side
  - Make engineering decisions
  - Maintain consistent coding practices and architectural standards
- Previously coordinated team tasks, Jira board, sprint planning, and overall development workflow as Team Leader.

## What I Learned:
- Advanced event-driven and data-driven programming
- Working with FMOD integration in Unity
- Working with custom UI Toolkit inspectors
- Using NavMesh systems for AI behaviour and navigation
- Creating procedural animations using DOTween
- Working with Unity's Particle System, shaders, and VFX
- Using Unity's New Input System
- Improving code quality through refactoring, optimisation, and architectural improvements
- Strengthening my understanding of design patterns
- Team coordination and technical leadership
- Working with Jira and Scrum-based project organisation

## Try the Early Beta:
https://dobra-job-studios.itch.io/naihe-bridge - itch.io game page
