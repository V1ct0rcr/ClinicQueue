export type AppointmentCardProps = {
  id: number;
  patientId: number;
  doctorId: number;
  appointmentDate: string;
  status: string;
};

export function AppointmentCard({
  id,
  patientId,
  doctorId,
  appointmentDate,
  status,
}: AppointmentCardProps) {
  return (
    <div
      style={{
        border: "1px solid #e2e8f0",
        borderRadius: 14,
        padding: 14,
        display: "grid",
        gap: 4,
      }}
    >
      <div style={{ fontWeight: 800 }}>Programare #{id}</div>
      <div style={{ fontSize: 13, color: "#64748b" }}>
        Data:{" "}
        {new Date(appointmentDate).toLocaleDateString("ro-RO", {
          year: "numeric",
          month: "long",
          day: "numeric",
        })}
      </div>
      <div style={{ fontSize: 13, color: "#64748b" }}>
        Doctor ID: {doctorId} | Pacient ID: {patientId}
      </div>
      <div style={{ fontSize: 13 }}>
        Status:{" "}
        <span
          style={{
            background: status === "Pending" ? "#fef9c3" : "#dcfce7",
            color: status === "Pending" ? "#854d0e" : "#166534",
            padding: "2px 8px",
            borderRadius: 8,
            fontWeight: 600,
          }}
        >
          {status}
        </span>
      </div>
    </div>
  );
}
