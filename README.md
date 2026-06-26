<div align="center">
  <h1>Pika</h1>

  <p>
    <b>A full desktop messaging system built with Avalonia, .NET, SQLite, MVVM, TCP sockets, live updates, friend invites, account recovery, and Linux desktop notifications.</b>
  </p>

  <p>
    <img alt=".NET" src="https://img.shields.io/badge/.NET_10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
    <img alt="C Sharp" src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" />
    <img alt="Avalonia" src="https://img.shields.io/badge/Avalonia_UI-8B5CF6?style=for-the-badge&logo=windowsterminal&logoColor=white" />
    <img alt="MVVM" src="https://img.shields.io/badge/MVVM-FF006E?style=for-the-badge" />
    <img alt="SQLite" src="https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white" />
    <img alt="TCP" src="https://img.shields.io/badge/TCP_Sockets-00C2A8?style=for-the-badge" />
  </p>

  <p>
    <a href="#features">
      <img alt="Features" src="https://img.shields.io/badge/Features-00C2A8?style=for-the-badge" />
    </a>
    <a href="#screenshots">
      <img alt="Screenshots" src="https://img.shields.io/badge/Screenshots-F59E0B?style=for-the-badge" />
    </a>
    <a href="#architecture">
      <img alt="Architecture" src="https://img.shields.io/badge/Architecture-7C3AED?style=for-the-badge" />
    </a>
    <a href="#how-it-works">
      <img alt="How it works" src="https://img.shields.io/badge/How_It_Works-2563EB?style=for-the-badge" />
    </a>
    <a href="#why-i-made-this">
      <img alt="Why I made this" src="https://img.shields.io/badge/Why_I_Made_This-FF3864?style=for-the-badge" />
    </a>
  </p>

</div>

---

## Overview

**Live Message App** is a desktop messenger built as a real client/server system, not just a UI mockup. It combines a modern Avalonia desktop client with local SQLite persistence and a TCP relay server for live packet broadcasting.

The app supports the core flows expected from a messaging product:

| Flow | What it does |
| --- | --- |
| Authentication | Login and registration screens backed by SQLite user records. |
| Messaging | Chat with selected friends, store messages locally, and receive live updates from the network. |
| Friend system | Send friend requests, accept/refuse incoming invites, and create a starter chat on acceptance. |
| Account recovery | Send password recovery emails through MailKit/SMTP. |
| Account deletion | Delete the local account data, notify the user by email, and broadcast a delete packet. |
| Notifications | Trigger Linux desktop notifications for new messages and friend requests. |

This is the biggest project in the collection because it touches UI architecture, local data modeling, network protocol design, async background receiving, email delivery, desktop integration, and multi-screen app navigation.

## Screenshots

Add screenshots here when you capture the app. Suggested order:

| Screen | Placeholder |
| --- | --- |
| Login | <img width="1200" height="720" alt="image" src="https://github.com/user-attachments/assets/c98a1b24-d57d-402d-875a-72260c09456d" /> |
| Register | <img width="1200" height="720" alt="image" src="https://github.com/user-attachments/assets/8ab14b8f-48a8-4039-8d6b-163fa04ef30d" /> |
| Main chat | <img width="1200" height="720" alt="image" src="https://github.com/user-attachments/assets/6bde3d56-9c42-41e6-aed5-516999422b4e" /> |
| Friend invites | <img width="1200" height="720" alt="image" src="https://github.com/user-attachments/assets/e4414172-4686-45b2-a8a5-77587470d875" /> |
| Settings/delete account | <img width="1200" height="720" alt="image" src="https://github.com/user-attachments/assets/b455dcbc-5f76-4d54-83ee-70af21866ac9" /> |
| Recovery email | <img width="1200" height="720" alt="image" src="https://github.com/user-attachments/assets/d4b9664c-8618-4db4-8d79-33007d9218f1" /> |


## Features

<table>
  <tr>
    <td><b>Desktop messenger UI</b></td>
    <td>Dark Avalonia interface with login, registration, recovery, chat, invites, settings, and account deletion screens.</td>
  </tr>
  <tr>
    <td><b>MVVM navigation</b></td>
    <td>Uses view models as page state and swaps the current screen through <code>Currentpage</code>.</td>
  </tr>
  <tr>
    <td><b>Local accounts</b></td>
    <td>Stores users, credentials, Gmail addresses, messages, and invites in SQLite.</td>
  </tr>
  <tr>
    <td><b>Live TCP updates</b></td>
    <td>Connects to a TCP server on port <code>8000</code> and broadcasts JSON packets for messages, invites, accepted requests, user creation, and deletion.</td>
  </tr>
  <tr>
    <td><b>Friend request system</b></td>
    <td>Users can search by username, send invites, accept/refuse requests, and start conversations.</td>
  </tr>
  <tr>
    <td><b>Conversation list</b></td>
    <td>Builds friend list entries from chat history and shows the latest message for each conversation.</td>
  </tr>
  <tr>
    <td><b>Chat bubbles</b></td>
    <td>Renders incoming and outgoing messages with separate alignment and colors.</td>
  </tr>
  <tr>
    <td><b>Email recovery</b></td>
    <td>Uses MailKit and SMTP to send account recovery messages.</td>
  </tr>
  <tr>
    <td><b>Desktop notifications</b></td>
    <td>Calls <code>notify-send</code> for new messages and friend requests on Linux desktops.</td>
  </tr>
  <tr>
    <td><b>Account deletion flow</b></td>
    <td>Deletes user data from users, chats, and invites, then broadcasts a delete packet.</td>
  </tr>
</table>

## Tech Stack

<p>
  <img alt=".NET 10" src="https://img.shields.io/badge/.NET_10-512BD4?style=flat-square&logo=dotnet&logoColor=white" />
  <img alt="C Sharp" src="https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white" />
  <img alt="Avalonia" src="https://img.shields.io/badge/Avalonia_UI-8B5CF6?style=flat-square" />
  <img alt="Fluent Theme" src="https://img.shields.io/badge/Fluent_Theme-0F766E?style=flat-square" />
  <img alt="CommunityToolkit MVVM" src="https://img.shields.io/badge/CommunityToolkit.MVVM-9333EA?style=flat-square" />
  <img alt="SQLite" src="https://img.shields.io/badge/Microsoft.Data.Sqlite-003B57?style=flat-square&logo=sqlite&logoColor=white" />
  <img alt="MailKit" src="https://img.shields.io/badge/MailKit-DC2626?style=flat-square" />
  <img alt="TCP" src="https://img.shields.io/badge/TCP_Sockets-0284C7?style=flat-square" />
  <img alt="JSON" src="https://img.shields.io/badge/System.Text.Json-F97316?style=flat-square" />
  <img alt="Linux notifications" src="https://img.shields.io/badge/notify--send-111827?style=flat-square&logo=linux&logoColor=white" />
</p>

| Layer | Technology | Role |
| --- | --- | --- |
| Client framework | `Avalonia 12` | Cross-platform desktop UI, windows, controls, styles, and data templates. |
| Runtime | `.NET 10` | Client and server target framework. |
| Language | `C#` | UI logic, services, networking, database access, and server code. |
| UI pattern | `CommunityToolkit.Mvvm` | Observable properties and relay commands. |
| Storage | `Microsoft.Data.Sqlite` | Local users, chats, and friend invite persistence. |
| Networking | `TcpClient`, `TcpListener` | Client connection and server broadcast relay. |
| Packet format | `System.Text.Json` | JSON packet serialization over TCP. |
| Email | `MailKit`, `MimeKit` | Recovery and account deletion emails. |
| Desktop integration | `notify-send` | Linux notification popups. |

## Architecture

```text
live-message-app/
|-- live-messagge-app/                 # Avalonia desktop client
|   |-- Program.cs                     # Avalonia app startup
|   |-- App.axaml                      # Fluent theme and view-model templates
|   |-- ViewLocator.cs                 # ViewModel-to-View fallback locator
|   |-- live-message-app.csproj        # Client dependencies and .NET target
|   |-- Assets/
|   |   `-- logo.jpg                   # App logo / notification icon
|   |-- Services/
|   |   |-- Network.cs                 # TCP client + JSON packet send/receive
|   |   |-- database.cs                # SQLite users, chats, invites, account operations
|   |   `-- Notifications.cs           # notify-send wrapper
|   |-- ViewModels/
|   |   |-- MainWindowViewModel.cs     # App shell, network receiver, packet routing
|   |   |-- LoginViewModel.cs          # Login flow
|   |   |-- RegisterViewModel.cs       # Registration flow
|   |   |-- RecoverViewModel.cs        # Password recovery email flow
|   |   `-- MainMenuViewModel.cs       # Messenger UI, invites, messages, settings
|   |-- Views/
|   |   |-- MainWindow.axaml           # App window and content host
|   |   |-- login.axaml                # Sign-in screen
|   |   |-- register.axaml             # Account creation screen
|   |   |-- recover.axaml              # Recovery screen
|   |   `-- mainmenu.axaml             # Main messenger interface
|   `-- databases/
|       |-- admin.db                   # Main SQLite database
|       `-- data.db                    # Additional local database artifact
|-- live-message-client/
|   `-- LiveMessage.Server/            # TCP broadcast server copy
|       |-- Program.cs
|       `-- LiveMessage.Server.csproj
`-- README.md
```

## System Design

### Client

The Avalonia client owns the actual product experience. It starts with `MainWindowViewModel`, connects to the local server, and displays `LoginViewModel` first.

```text
MainWindow
   |
   `-- ContentControl -> Currentpage
                         |
                         |-- LoginViewModel
                         |-- RegisterViewModel
                         |-- RecoverViewModel
                         `-- MainMenuViewModel
```

`App.axaml` maps each view model to its view through Avalonia data templates, so changing `Currentpage` changes the active screen.

### Server

The server is intentionally small:

1. Open a `TcpListener` on port `8000`.
2. Accept clients.
3. Store connected clients in memory.
4. Start a task per client.
5. Read raw packet bytes.
6. Broadcast the packet to every other connected client.

The server does not currently validate packets or persist state. It behaves as a relay; the clients handle local persistence.

### Local Persistence

The client database stores three core tables:

| Table | Purpose |
| --- | --- |
| `users` | Account id, full name, username, password, and Gmail address. |
| `chats` | Message sender, receiver, text body, and conversation order. |
| `invites` | Pending friend requests from one user id to another. |

## Packet Protocol

Messages are serialized as JSON lines through `System.Text.Json`.

```csharp
public class packet
{
    public string Type { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string Text { get; set; }
}
```

| Packet type | Sent when | Receiver behavior |
| --- | --- | --- |
| `message` | A user sends a chat message. | Store message locally, refresh open chat/friend list, show desktop notification. |
| `request` | A user sends a friend invite. | Store invite locally, refresh pending invite list, show desktop notification. |
| `accepted` | A user accepts a friend invite. | Create starter chat and refresh friend list. |
| `add_user` | A new account is registered. | Mirror the user into the local database. |
| `delete` | A user deletes their account. | Remove that user and related chats/invites locally. |

## User Flows

### Register

1. User enters full name, username, Gmail, password, and confirmation.
2. The view model validates matching passwords and Gmail format.
3. The user is inserted into SQLite.
4. An `add_user` packet is broadcast so other connected clients can mirror the account.

### Login

1. User enters username and password.
2. SQLite checks credentials.
3. A valid id moves the app into the main messenger screen.
4. Invalid credentials show an error state on the login card.

### Send a Message

1. User selects a friend from the left conversation list.
2. Previous messages load from `chats`.
3. User writes a message and presses `Send`.
4. Message is inserted locally with a conversation order.
5. A `message` packet is sent through the TCP client.
6. Other clients receive it, store it, refresh visible state, and show a notification.

### Friend Invites

1. User opens the invite panel.
2. User enters a username and sends a request.
3. A `request` packet is broadcast.
4. The target client stores the invite and shows it in the pending invite list.
5. Accepting creates a starter chat and sends an `accepted` packet.

### Password Recovery

1. User enters a username on the recovery screen.
2. The app finds the Gmail address for that account.
3. MailKit sends a recovery email.
4. The UI reports that the email was sent.

### Account Deletion

1. User opens settings.
2. User presses `Delete`.
3. The app sends a deletion email.
4. Local user, invites, and chat rows are deleted.
5. A `delete` packet is broadcast.
6. Other clients remove the deleted user locally.

## Run Locally

### 1. Start The TCP Server

From the nested server project:

```bash
cd live-message-client/LiveMessage.Server
dotnet run
```

The server listens on:

```text
0.0.0.0:8000
```

### 2. Start The Avalonia Client

In another terminal:

```bash
cd live-messagge-app
dotnet restore
dotnet run
```

The client currently tries to connect to:

```text
127.0.0.1:8000
```

### 3. Run Multiple Clients

To test live messaging on one machine:

1. Start the server once.
2. Run two client instances.
3. Register/login as two different users.
4. Send an invite from one user to the other.
5. Accept the invite.
6. Start messaging.

## Configuration Notes

Some paths and values are currently hard-coded because this project is still in prototype form:

| Area | Current state | Better production direction |
| --- | --- | --- |
| Server address | Client connects to `127.0.0.1:8000`. | Move host/port into config. |
| SQLite path | Database service uses an absolute local path. | Use app data directory or relative config. |
| SMTP credentials | Mail sender credentials are inside source code. | Move to environment variables or user secrets. |
| Notification icon | Notification service uses a local image path. | Resolve from packaged assets. |
| Passwords | Stored as plain text in SQLite. | Hash and salt passwords. |

## Security And Production Readiness

This project is strong as a learning and portfolio build because it connects many real app layers. It is not production-secure yet.

Important next steps before treating it like a real messenger:

| Risk | Why it matters | Fix |
| --- | --- | --- |
| Plain-text passwords | Anyone with DB access can read passwords. | Use a password hashing algorithm like Argon2, bcrypt, or PBKDF2. |
| Hard-coded SMTP secret | Secrets in source can leak. | Use environment variables, user secrets, or a secret manager. |
| No packet authentication | Any connected client can send any packet shape. | Add authenticated sessions and server-side validation. |
| Trusting clients for persistence | Clients mirror state locally without server authority. | Move authoritative user/message state to a backend database. |
| No TLS | TCP packets are sent without encryption. | Use TLS or a secure transport. |
| Blocking network reads | Receiver loop reads directly from the stream. | Add cancellation, reconnection, framing, and better error handling. |

## Why I Made This

being honest , i had no buisness to be here , neither learn avalonia , c# or the linux Dbus system , but i thought it would be fun to learn something new and
shape it into somethign deployable , well so far the app is not a "production level" software , but it's tested and can be usd descently as a messaging app,
the most i enjoyed abt this project was the packet sending and recienving , it was fun to customize your packets and send them in flows , i had wayyyyy more 
ideas to add but my schedue is tight and i have the malware tester on hold , so maybe i ll ocme later to add more features from time to time , a bit simple  as
my first contact with c# and avalonia but was enjoyable a lot 

## Current Status

| Area | Status |
| --- | --- |
| Avalonia desktop shell | Working |
| Login/register screens | Working |
| Password recovery screen | Working |
| Main messenger layout | Working |
| Conversation list | Working |
| Friend invites | Working |
| Local message persistence | Working |
| TCP broadcast server | Working prototype |
| Desktop notifications | Working on Linux with `notify-send` |
| Account deletion | Working prototype |
| Production authentication/security | Needs hardening |

## What This Project Shows

This repo demonstrates more than one library or one screen. It shows:

| Skill | Evidence in the project |
| --- | --- |
| Desktop UI architecture | Avalonia views, data templates, MVVM page switching. |
| State management | Observable properties, selected conversation state, invite panels, settings modal. |
| Database work | SQLite queries for users, chats, invites, last messages, and account deletion. |
| Networking | TCP client/server, packet serialization, live packet receiving. |
| Async thinking | Background receive loop updates UI state through Avalonia dispatcher. |
| Product thinking | Login, recovery, registration, messaging, invites, deletion, notifications. |
| Debugging real systems | Handles multiple moving pieces that must stay synchronized. |

---

<div align="center">
  <sub>Built with Avalonia, C#, SQLite, TCP sockets, MVVM, MailKit, and the stubborn idea that a desktop app should feel alive.</sub>
</div>
