import { getUser } from "../services/auth";

export function AdminDashboardPage() {
    const user = getUser();

    return (
        <div style={{ display: "grid", gap: 12 }}>
            <h1 style={{ margin: 0 }}>Admin Dashboard</h1>
            <div style={{ color: "#475569" }}>Doar admin are acces aici (mock).</div>

            <div
                style={{
                    padding: 14,
                    borderRadius: 14,
                    border: "1px solid #e2e8f0",
                    background: "#f8fafc",
                }}
            >
                <div>
                    <b>User:</b> {user?.username}
                </div>
                <div>
                    <b>Role:</b> {user?.role}
                </div>
            </div>

            <ul style={{ margin: 0, paddingLeft: 18, color: "#0f172a" }}>
                <li>Gestionează doctori (mock)</li>
                <li>Vezi programări (mock)</li>
                <li>Setări aplicație (mock)</li>
            </ul>
        </div>
    );
}
