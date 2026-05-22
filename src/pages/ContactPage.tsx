import React, { useState } from "react";

export function ContactPage() {
    const [name, setName] = useState("");
    const [message, setMessage] = useState("");
    const [sent, setSent] = useState(false);

    function onSubmit(e: React.FormEvent) {
        e.preventDefault();
        setSent(true);
    }

    return (
        <div style={{ display: "grid", gap: 12 }}>
            <h1 style={{ fontSize: 44, margin: 0 }}>Contact</h1>
            <p style={{ margin: 0, color: "#475569" }}>
                Pagina demo. Mesajul nu se trimite real (mock).
            </p>

            <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: 12 }}>
                <div style={{ border: "1px solid #e2e8f0", borderRadius: 14, padding: 16, background: "white" }}>
                    <div style={{ fontWeight: 800, marginBottom: 8 }}>Date clinică</div>
                    <div style={{ color: "#334155", lineHeight: 1.7 }}>
                        <div><b>Telefon:</b> +373 00 000 000</div>
                        <div><b>Email:</b> contact@clinicqueue.md</div>
                        <div><b>Adresă:</b> Str. Exemplu 10, Chișinău</div>
                        <div><b>Program:</b> L-V 08:00–18:00</div>
                    </div>
                </div>

                <div style={{ border: "1px solid #e2e8f0", borderRadius: 14, padding: 16, background: "white" }}>
                    <div style={{ fontWeight: 800, marginBottom: 8 }}>Scrie-ne</div>

                    {sent ? (
                        <div
                            style={{
                                padding: 12,
                                borderRadius: 12,
                                background: "#ecfdf5",
                                border: "1px solid #a7f3d0",
                                color: "#065f46",
                                fontWeight: 700,
                            }}
                        >
                            Mesaj trimis (mock) ✅
                        </div>
                    ) : (
                        <form onSubmit={onSubmit} style={{ display: "grid", gap: 10 }}>
                            <label style={{ display: "grid", gap: 6 }}>
                                <span style={{ fontSize: 13, fontWeight: 800 }}>Nume</span>
                                <input
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                    placeholder="Numele tău"
                                    style={{ padding: "10px 12px", borderRadius: 12, border: "1px solid #e2e8f0" }}
                                />
                            </label>

                            <label style={{ display: "grid", gap: 6 }}>
                                <span style={{ fontSize: 13, fontWeight: 800 }}>Mesaj</span>
                                <textarea
                                    value={message}
                                    onChange={(e) => setMessage(e.target.value)}
                                    placeholder="Cu ce te putem ajuta?"
                                    rows={4}
                                    style={{ padding: "10px 12px", borderRadius: 12, border: "1px solid #e2e8f0", resize: "vertical" }}
                                />
                            </label>

                            <button
                                type="submit"
                                disabled={!name.trim() || !message.trim()}
                                style={{
                                    marginTop: 6,
                                    padding: "12px 14px",
                                    borderRadius: 12,
                                    border: "none",
                                    background: !name.trim() || !message.trim() ? "#94a3b8" : "black",
                                    color: "white",
                                    fontWeight: 800,
                                    cursor: !name.trim() || !message.trim() ? "not-allowed" : "pointer",
                                }}
                            >
                                Trimite
                            </button>
                        </form>
                    )}
                </div>
            </div>
        </div>
    );
}
