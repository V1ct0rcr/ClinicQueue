export type DoctorCardProps = {
  id: number;
  name: string;
  specialty: string;
  rating: number;
  location: string;
};

export function DoctorCard({ name, specialty, rating, location }: DoctorCardProps) {
  return (
    <div
      style={{
        border: "1px solid #e2e8f0",
        borderRadius: 14,
        padding: 14,
      }}
    >
      <div style={{ fontWeight: 800 }}>{name}</div>
      <div style={{ fontSize: 13, color: "#64748b" }}>{specialty}</div>
      <div style={{ marginTop: 10, display: "flex", gap: 8, flexWrap: "wrap" }}>
        <span
          style={{
            fontSize: 13,
            background: "#f1f5f9",
            padding: "4px 8px",
            borderRadius: 8,
          }}
        >
          ⭐ {rating}
        </span>
        <span style={{ fontSize: 13, color: "#475569" }}>{location}</span>
      </div>
    </div>
  );
}
