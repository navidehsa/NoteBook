# NoteBook Application

This is a simple NoteBook application built using Azure Functions and CosmosDB. The application allows users to create, read, update, and delete personal notes, with additional features like search and automated email notifications.

## Features

- **CRUD Operations**: Perform create, read, update, and delete operations on personal notes.
- **Search Functionality**: Search notes by keywords.
- **Daily Summary Email**: Sends a summary of the last 5 notes every night at 4 AM.
- **Optional Reminder**: Users can set reminders to receive a specific note via email at a specified time.

## Technologies Used

- **Azure Functions**: Serverless architecture to handle the backend logic.
- **Azure CosmosDB**: NoSQL database for storing notes data.
- **SendGrid (or any Email Service)**: For sending automated email notifications and reminders.
- **Endpoint Helper**: Utility to assist with API calls and routing within the Azure Function.

## How It Works

1. **Azure Function App**: The core of the application. Handles HTTP requests for note management and scheduling the email tasks.
2. **CosmosDB**: Stores user notes in a scalable NoSQL format.
3. **Scheduled Email Summary**: A timer-triggered Azure Function runs every night at 4 AM to send a summary email of the last 5 notes.
4. **Reminder Emails**: Optional email reminders sent based on user-specified times.

## Endpoints

- `POST /notes`: Create a new note.
- `GET /notes`: Get all notes or search by keyword.
- `GET /notes/{id}`: Get a specific note by ID.
- `PUT /notes/{id}`: Update an existing note.
- `DELETE /notes/{id}`: Delete a note.
- `POST /notes/reminder`: Set a reminder to receive a note via email.

## Deployment

This application can be deployed on **Azure Functions** and is fully scalable with **CosmosDB**. Email functionalities can be handled by services like **SendGrid** integrated with Azure.

---

### Get Started

1. Clone this repository.
2. Set up your Azure resources (Function App, CosmosDB, SendGrid).
3. Deploy the Function App and configure the necessary environment variables for database and email integration.

