import { Link } from "react-router-dom";

export function HomePage() {
    return (
        <div style={{ display: "grid", gap: 12 }}>
            <h1 style={{ fontSize: 24, fontWeight: 900 }}>Bine ai venit</h1>
            <p style={{ color: "#475569" }}>
                ClinicQueue este o aplicație demo pentru programări medicale. Momentan folosim date simulate (mock), ulterior o conectăm la backend.
            </p>

            <div style={{ display: "grid", gap: 12, gridTemplateColumns: "repeat(auto-fit, minmax(220px, 1fr))" }}>
                <div style={{ border: "1px solid #e2e8f0", borderRadius: 14, padding: 14 }}>
                    <div style={{ fontWeight: 800 }}>Doctori</div>
                    <div style={{ fontSize: 13, color: "#64748b", marginTop: 6 }}>Listă doctori + specialități (mock).</div>
                    <Link
                        to="/doctors"
                        style={{ display: "inline-block", marginTop: 10, background: "black", color: "white", padding: "8px 10px", borderRadius: 10, fontSize: 13, textDecoration: "none" }}
                    >
                        Vezi doctorii
                    </Link>
                </div>

                <div style={{ border: "1px solid #e2e8f0", borderRadius: 14, padding: 14 }}>
                    <div style={{ fontWeight: 800 }}>Programare</div>
                    <div style={{ fontSize: 13, color: "#64748b", marginTop: 6 }}>Formular cu validări (mock).</div>
                    <Link
                        to="/book"
                        style={{ display: "inline-block", marginTop: 10, background: "black", color: "white", padding: "8px 10px", borderRadius: 10, fontSize: 13, textDecoration: "none" }}
                    >
                        Creează programare
                    </Link>
                </div>
            </div>
        </div>
    );
}
