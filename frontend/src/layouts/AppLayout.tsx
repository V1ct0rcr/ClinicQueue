import React, { useEffect, useMemo, useState } from "react";
import { NavLink, Outlet, useNavigate } from "react-router-dom";
import { getUser, logout, type AuthUser } from "../services/auth";

const container: React.CSSProperties = {
    width: "100%",
    maxWidth: 1200,
    margin: "0 auto",
    padding: "0 16px",
};

const linkStyle = ({ isActive }: { isActive: boolean }): React.CSSProperties => ({
    display: "inline-block",
    padding: "10px 12px",
    borderRadius: 10,
    textDecoration: "none",
    fontSize: 13,
    fontWeight: 800,
    color: isActive ? "white" : "#334155",
    background: isActive ? "black" : "transparent",
});

function Chip({ children }: { children: React.ReactNode }) {
    return (
        <span
            style={{
                padding: "6px 10px",
                borderRadius: 999,
                border: "1px solid #e2e8f0",
                background: "#f8fafc",
                fontSize: 12,
                fontWeight: 800,
                color: "#0f172a",
                lineHeight: 1,
            }}
        >
            {children}
        </span>
    );
}

const btnBase: React.CSSProperties = {
    padding: "8px 12px",
    borderRadius: 12,
    fontWeight: 900,
    cursor: "pointer",
    lineHeight: 1,
    display: "inline-flex",
    alignItems: "center",
    justifyContent: "center",
    minWidth: 92, // ca să nu pară „gol”
};

export function AppLayout() {
    const navigate = useNavigate();
    const [user, setUserState] = useState<AuthUser | null>(() => getUser());

    // Actualizează UI-ul când se schimbă localStorage (login/logout)
    useEffect(() => {
        const sync = () => setUserState(getUser());

        sync(); // ✅ la montare, citește userul imediat

        window.addEventListener("authchange", sync); // ✅ evenimentul nostru
        window.addEventListener("focus", sync);
        window.addEventListener("storage", sync);

        return () => {
            window.removeEventListener("authchange", sync);
            window.removeEventListener("focus", sync);
            window.removeEventListener("storage", sync);
        };
    }, []);

    const isAdmin = useMemo(() => user?.role === "Admin", [user]);

    function onLogout() {
        logout();
        setUserState(null);
        navigate("/", { replace: true });
    }

    function goLogin() {
        navigate("/login", { replace: false });
    }

    return (
        <div style={{ minHeight: "100vh", background: "#eef2f7", color: "#0f172a" }}>
            <header style={{ background: "white", borderBottom: "1px solid #e2e8f0" }}>
                <div
                    style={{
                        ...container,
                        paddingTop: 12,
                        paddingBottom: 12,
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "space-between",
                        gap: 12,
                        flexWrap: "wrap",
                    }}
                >
                    <div style={{ fontWeight: 900 }}>ClinicQueue</div>

                    <div style={{ display: "flex", alignItems: "center", gap: 10, flexWrap: "wrap" }}>
                        <div style={{ fontSize: 13, color: "#475569" }}>Programări medicale (mock)</div>

                        <Chip>{user ? `${user.username} (${user.role})` : "guest"}</Chip>

                        {user ? (
                            <button
                                type="button"
                                onClick={onLogout}
                                aria-label="Logout"
                                style={{
                                    ...btnBase,
                                    border: "2px solid #0f172a",
                                    background: "white",
                                    color: "#0f172a",
                                }}
                            >
                                Logout
                            </button>
                        ) : (
                            <button
                                type="button"
                                onClick={goLogin}
                                aria-label="Login"
                                style={{
                                    ...btnBase,
                                    border: 0,
                                    background: "black",
                                    color: "white",
                                }}
                            >
                                Login
                            </button>
                        )}
                    </div>
                </div>
            </header>

            <div style={{ ...container, paddingTop: 16, paddingBottom: 16, display: "grid", gap: 16 }}>
                <nav
                    style={{
                        background: "white",
                        border: "1px solid #e2e8f0",
                        borderRadius: 14,
                        padding: 12,
                        display: "flex",
                        gap: 8,
                        flexWrap: "wrap",
                    }}
                >
                    <NavLink to="/" style={linkStyle} end>
                        Home
                    </NavLink>
                    <NavLink to="/doctors" style={linkStyle}>
                        Doctors
                    </NavLink>
                    <NavLink to="/book" style={linkStyle}>
                        Book
                    </NavLink>
                    <NavLink to="/appointments" style={linkStyle}>
                        Appointments
                    </NavLink>
                    <NavLink to="/contact" style={linkStyle}>
                        Contact
                    </NavLink>

                    {isAdmin && (
                        <NavLink to="/admin" style={linkStyle}>
                            Admin
                        </NavLink>
                    )}
                </nav>

                <main
                    style={{
                        background: "white",
                        border: "1px solid #e2e8f0",
                        borderRadius: 14,
                        padding: 16,
                        width: "100%",
                    }}
                >
                    <Outlet />
                </main>
            </div>

            <footer style={{ background: "white", borderTop: "1px solid #e2e8f0" }}>
                <div style={{ ...container, paddingTop: 10, paddingBottom: 10, fontSize: 12, color: "#64748b" }}>
                    © {new Date().getFullYear()} ClinicQueue
                </div>
            </footer>
        </div>
    );
}
