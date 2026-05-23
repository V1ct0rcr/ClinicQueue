import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useApi } from "../context/ApiContext";
import { isAuthenticated } from "../services/auth";
import {
  AppointmentCard,
  type AppointmentCardProps,
} from "../components/AppointmentCard";

export function AppointmentsPage() {
  const api = useApi();
  const navigate = useNavigate();
  const [appointments, setAppointments] = useState<AppointmentCardProps[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    if (!isAuthenticated()) {
      navigate("/login", {
        replace: true,
        state: { from: "/appointments" },
      });
      return;
    }

    api
      .get<AppointmentCardProps[]>("/appointments")
      .then((res) => setAppointments(res.data))
      .catch((err) => {
        if (err.response?.status === 401) {
          navigate("/login", {
            replace: true,
            state: { from: "/appointments" },
          });
        } else {
          setError("Nu s-au putut încărca programările.");
        }
      })
      .finally(() => setLoading(false));
  }, [api, navigate]);

  if (loading) return <p>Se încarcă...</p>;
  if (error) return <p style={{ color: "#dc2626" }}>{error}</p>;
  if (appointments.length === 0)
    return <p style={{ color: "#475569" }}>Nu ai programări înregistrate.</p>;

  return (
    <div style={{ display: "grid", gap: 12 }}>
      <h1 style={{ margin: 0 }}>Programările mele</h1>

      <div style={{ display: "grid", gap: 10 }}>
        {appointments.map((appointment) => (
          <AppointmentCard key={appointment.id} {...appointment} />
        ))}
      </div>
    </div>
  );
}
