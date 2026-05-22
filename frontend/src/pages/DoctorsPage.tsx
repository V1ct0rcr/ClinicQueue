type Doctor = {
    id: string;
    name: string;
    specialty: string;
    rating: number;
    location: string;
};

const doctors: Doctor[] = [
    { id: "d1", name: "Dr. Ana Rusu", specialty: "Cardiologie", rating: 4.7, location: "Chișinău" },
    { id: "d2", name: "Dr. Mihai Popa", specialty: "Dermatologie", rating: 4.5, location: "Bălți" },
    { id: "d3", name: "Dr. Irina Cojocaru", specialty: "ORL", rating: 4.6, location: "Chișinău" },
];

export function DoctorsPage() {
    return (
        <div style={{ display: "grid", gap: 12 }}>
            <h1 style={{ fontSize: 20, fontWeight: 900 }}>Doctori</h1>

            <div style={{ display: "grid", gap: 12, gridTemplateColumns: "repeat(auto-fit, minmax(240px, 1fr))" }}>
                {doctors.map((d) => (
                    <div key={d.id} style={{ border: "1px solid #e2e8f0", borderRadius: 14, padding: 14 }}>
                        <div style={{ fontWeight: 800 }}>{d.name}</div>
                        <div style={{ fontSize: 13, color: "#64748b" }}>{d.specialty}</div>

                        <div style={{ marginTop: 10, display: "flex", gap: 8, alignItems: "center", flexWrap: "wrap" }}>
                            <span style={{ fontSize: 13, background: "#f1f5f9", padding: "4px 8px", borderRadius: 8 }}>
                                ⭐ {d.rating}
                            </span>
                            <span style={{ fontSize: 13, color: "#475569" }}>{d.location}</span>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}
