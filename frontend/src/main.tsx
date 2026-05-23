import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";

import { ApiProvider } from "./context/ApiContext";
import { AppLayout } from "./layouts/AppLayout";
import { HomePage } from "./pages/HomePage";
import { DoctorsPage } from "./pages/DoctorsPage";
import { BookAppointmentPage } from "./pages/BookAppointmentPage";
import { AppointmentsPage } from "./pages/AppointmentsPage";
import { ContactPage } from "./pages/ContactPage";
import { LoginPage } from "./pages/LoginPage";
import { AdminDashboardPage } from "./pages/AdminDashboardPage";
import { ServerDownPage } from "./pages/ServerDownPage";
import { ProtectedRoute } from "./components/ProtectedRoute";

import "./index.css";

const router = createBrowserRouter([
  {
    path: "/500",
    element: <ServerDownPage />,
  },

  {
    element: (
      <ApiProvider>
        <AppLayout />
      </ApiProvider>
    ),
    children: [
      { path: "/", element: <HomePage /> },
      { path: "/doctors", element: <DoctorsPage /> },
      { path: "/book", element: <BookAppointmentPage /> },
      { path: "/appointments", element: <AppointmentsPage /> },
      { path: "/contact", element: <ContactPage /> },
      { path: "/login", element: <LoginPage /> },
      {
        path: "/admin",
        element: (
          <ProtectedRoute requiredRole="Admin">
            <AdminDashboardPage />
          </ProtectedRoute>
        ),
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
