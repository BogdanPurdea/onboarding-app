# Assumptions

## About the Users

* **Who uses the application?** (e.g., new employee, HR, managers, colleagues, etc.)
  - The initial main user of the application is the new employee, who is unfamiliar with the working environment at the company that hired them. Authentication is not initially part of the application, but if in the future the application is extended for use by other types of users, I see authentication being required. I chose to focus on the new employee as the user since I wanted to build a web application that I'd personally use and that I thought would be of help in the first month of work.

* **What does the user already know when opening the app for the first time?**
  - All the new employee knows from the HR person is that this app is supposed to provide an onboarding experience throughout the first month, as well as provide information about the schedule and their coworkers.

---

## About the Data

* **Who enters the information into the application?**
  - DB managers are responsible for populating the database with the appropriate information and maintaining it. They handle what information goes into the database.
  - The information displayed on the client side will also be modifiable through a configuration file handled by content managers. Content managers are responsible for updating the information displayed on the client side through the configuration file. This did not end up being implemented by the time of submission and was added to the `WHAT_I_WOULD_DO_NEXT.md` as a feature to be built in the future.

* **When is the information added?**
  - Initial information is assumed to exist prior to the deployment of the application for onboarding usage. DB managers will add, update, and remove information from the database as changes are decided upon, which reaches the client side on the next API request. Client-side configuration-based updates can be made by content managers.

* **What happens if information is missing or incorrect?**
  - If the information is missing or incorrect, initially an error will be thrown and handled until the information is added or corrected by either a DB manager or a content manager, with the user being redirected to the previous URL visited. This should allow the user to get back on track. Later, I would add additional error handling, such as trying multiple times to get the correct information, or trying to use some information from the config file if it's available, before giving up and redirecting the user.

---

## About the Context

* **What device does the new employee use on the first day?**
  - The app should be accessible on both desktop computers and mobile devices. The frontend design is developed with a mobile-first approach.

* **Do they have access to the application before their first working day?**
  - The new employee should have access to the application before their first working day.
  - The application will be accessible through a web browser at a link provided by the HR person as soon as possible after the hiring of the new employee has been made.
