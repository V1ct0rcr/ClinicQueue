import React, { useMemo, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { setUser, type Role } from "../services/auth";

type Option = { label: string; username: string; role: Role; hint: string };

export function LoginPage() {
    const navigate = useNavigate();
    const location = useLocation() as { state?: { from?: string } };

    const options: Option[] = useMemo(
        () => [
            { label: "Guest", username: "guest", role: "guest", hint: "fără drepturi" },
            { label: "User", username: "user", role: "user", hint: "utilizator normal" },
            { label: "Admin", username: "admin", role: "admin", hint: "acces la /admin" },
        ],
        []
    );

    const [selected, setSelected] = useState<Option>(options[0]);

    function onSubmit(e: React.FormEvent) {
        e.preventDefault();
        setUser({ username: selected.username, role: selected.role });

        // dacă ProtectedRoute a trimis { state: { from: "/admin" } }, te întoarce acolo
        const backTo = location.state?.from ?? "/";
        navigate(backTo, { replace: true });
    }

    return (
        <div style={{ display: "grid", gap: 12 }}>
            <h1 style={{ margin: 0, fontSize: 56 }}>Login (mock)</h1>
            <div style={{ color: "#475569", fontSize: 18 }}>
                Alege un user de test. Nu există backend, salvăm în <code>localStorage</code>.
            </div>

            <form onSubmit={onSubmit} style={{ display: "grid", gap: 12, maxWidth: 720 }}>
                <div style={{ display: "grid", gap: 10 }}>
                    <div style={{ fontSize: 18, fontWeight: 900 }}>Alege rol</div>

                    <div
                        style={{
                            display: "grid",
                            gridTemplateColumns: "repeat(3, minmax(0, 1fr))",
                            gap: 10,
                        }}
                    >
                        {options.map((o) => {
                            const active = o.role === selected.role;

                            return (
                                <button
                                    key={o.role}
                                    type="button"
                                    onClick={() => setSelected(o)}
                                    style={{
                                        textAlign: "left",
                                        padding: 14,
                                        borderRadius: 14,
                                        border: active ? "2px solid #0f172a" : "1px solid #e2e8f0",
                                        background: active ? "#0f172a" : "white",
                                        color: active ? "white" : "#0f172a",
                                        cursor: "pointer",
                                    }}
                                >
                                    <div style={{ fontWeight: 900, fontSize: 16 }}>{o.label}</div>
                                    <div style={{ fontSize: 13, opacity: active ? 0.9 : 0.75 }}>{o.hint}</div>
                                </button>
                            );
                        })}
                    </div>

                    <div
                        style={{
                            padding: 12,
                            borderRadius: 14,
                            border: "1px solid #e2e8f0",
                            background: "#f8fafc",
                            color: "#334155",
                            fontSize: 14,
                        }}
                    >
                        Selectat: <b>{selected.username}</b> (role: <b>{selected.role}</b>)
                    </div>
                </div>

                <button
                    type="submit"
                    style={{
                        padding: "12px 14px",
                        borderRadius: 14,
                        border: "1px solid #0f172a",
                        background: "#0f172a",
                        color: "white",
                        fontWeight: 900,
                        cursor: "pointer",
                        width: "fit-content",
                    }}
                >
                    Intră ca {selected.label}
                </button>
            </form>
        </div>
    );
}
