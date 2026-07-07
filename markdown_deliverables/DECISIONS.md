# Decisions

## Product Decisions

### Which features did you include?
* **Schedule Viewer**: A visual grid displaying the weekly schedule differentiating between remote and office days.
* **Team Directory**: Lists department colleagues and provides links to contact them through Slack, email, or Google Meet.
* **Onboarding Checklist & Progress Tracking**: Trackable onboarding tasks split into weekly tasks for the first month, with instruction drawers that display task instructions.
* **LLM Chat Drawer**: A side-panel chatbot powered by the local Gemma4 model through Ollama. I prioritized this as a feature to allow users to find information using traditional dashboard navigation or a natural language chat, depending on their preference. I also decided to use Gemma4 for demo purposes because it is an open-source model and can be run locally. With a proprietary model, an API key is required, either from the user or the company.
* **Anonymous DB-Based Persistence**: Saves user checklist progress in the database with an assigned `sessionTokens` based on department role, which is also used for retrieval. I prioritized this as an alternative to user authentication, letting new hires jump straight into the application without signup barriers.

### How did you prioritize them?
* I prioritized building something functional in the first place, with more complex features to be added in the future. I focused on what I considered essential features in the context of the project requirements and my idea for the application. As such, the weekly schedule, coworker contacts, and task checklist became my initial focus and the base of the application, with DB-based persistence following shortly after to account for the lack of Authentication.
* The dark mode theme inclusion is a personal priority because I prefer dark themes as a user of any application, and the LLM integration is a feature I usually like to play with. This was a personal preference, not a strictly essential feature.

### Which features did you intentionally leave out of scope?
* **Interactive Instruction Tutorials**: Left out a more complex feature that I would love to have as a new employee. The idea was to have an interactable instructions/tutorial view instead of just the instruction drawer that would provide a complex flow to keep the user engaged, provide solid foundations of how things are done within their department, and teach them how to use tools. I knew from the beginning this was one of the more complex undertakings, that I would need complex task and instruction data for, so since the database contains simple mocked data just so I can showcase the application, I added this to the future features list (`WHAT_I_WOULD_DO_NEXT.md`).
* **Complete Server-Side CRUD Operations**: I chose to implement only the routes currently needed for the application to work with the mocked data (GET endpoints for departments, tasks, and task instructions, and a POST endpoint for progress syncing). I wanted to keep the complexity to a minimum, not implementing unnecessary CRUD operations just for the sake of it.
* **User Authentication**: Bypassed to streamline access for new hires. Building OAuth 2.0 or custom auth would add significant complexity that was not required for the scope of this project.

---

## Technical Decisions

### Why did you choose this database structure?
* Since I used Entity Framework to build the database code-first through migrations, it grew over time with each feature. Initially, I had a simple schema with just departments and onboarding tasks and task prerequisites. The additional tables were added as features were implemented, such as contact profiles, task instructions, and progress tracking.

#### Schema Details:
1. **Departments**: Stores department details and weekly office/remote schedules for each department.
2. **Department Contacts**: Coworker contact profiles linked to a department.
3. **Onboarding Tasks**: General and department-specific checklist task items.
4. **Task Prerequisites**: Self-referencing link table representing task dependency chains.
5. **Task Instructions**: Step-by-step instruction guidelines linked to tasks.
6. **Onboarding Progresses**: Stores task completions anonymously scoped to a department-specific `SessionToken` UUID.

### Why did you choose these libraries/frameworks?
* **Backend**: I chose **.NET (C#)** for the backend because I have previously used Entity Framework for code-first DB creation and management. I also consider it a great development environment for API servers.
* **Frontend**: I chose **React (TypeScript)** for the frontend because I recently started learning it, I prefer it over Angular and saw this project as a great opportunity to practice building React frontends. I also find React to be better supported and provide more options for building modern UIs.
* **Styling**: I chose **TailwindCSS** because it allows building responsive, mobile-first UIs quickly and easily integrates a native dark mode theme.
* **Code Generation**: I chose this stack knowing that AI coding agents handle C# and React well, and I am familiar with prompting techniques to get useful results within a single prompt for a given subtask, which significantly accelerated development.

### If you had more time, what would you build differently?
* I would take more time to think about the state of the final application I want to build and to define clear possible extension paths. For example, if I wanted the final product to include role-based features, I would have considered Authentication as a priority from the beginning. For a prototype, I did not consider the additional complexity tradeoff worth it.
* I would also consider a test-driven development cycle, since it's something I wanted to learn how to do properly for a while.

---

## UX Decisions

### Why did you choose this user flow?
* I chose this flow because this is how I imagined my personal experience as a user of this application. I wanted to provide the user with clear information about the onboarding process and allow the tracking of progress of interactable tasks. The fully interactable tutorial part I intentionally left out of the scope of implementation for the reasons I mentioned before.

### Did you test it with anyone?
* Yes, I asked a friend of mine who is a .NET software engineer for feedback on the web application's architecture and user experience.

### What changed after receiving feedback?
* The feedback I received was positive regarding the user experience and frontend design, with the caveat of suggesting a built-in note-taking feature. Besides that, he suggested implementing better error handling and more validation for the backend API.
