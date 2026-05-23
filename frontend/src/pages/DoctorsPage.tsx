import { useEffect, useState } from "react";
import { useApi } from "../context/ApiContext";
import { DoctorCard, type DoctorCardProps } from "../components/DoctorCard";

export function DoctorsPage() {
  const api = useApi();
  const [doctors, setDoctors] = useState<DoctorCardProps[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    api
      .get<DoctorCardProps[]>("/doctors")
      .then((res) => setDoctors(res.data))
      .catch(() => setError("Nu s-au putut încărca doctorii."))
      .finally(() => setLoading(false));
  }, [api]);

  if (loading) return <p>Se încarcă...</p>;
  if (error) return <p style={{ color: "#dc2626" }}>{error}</p>;
  if (doctors.length === 0) return <p>Nu există doctori înregistrați.</p>;

  return (
    <div style={{ display: "grid", gap: 12 }}>
      <h1 style={{ fontSize: 20, fontWeight: 900, margin: 0 }}>Doctori</h1>

      <div
        style={{
          display: "grid",
          gap: 12,
          gridTemplateColumns: "repeat(auto-fit, minmax(240px, 1fr))",
        }}
      >
        {doctors.map((doctor) => (
          <DoctorCard key={doctor.id} {...doctor} />
        ))}
      </div>
    </div>
  );
}
