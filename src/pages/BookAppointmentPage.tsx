import { useState } from "react";

export function BookAppointmentPage() {
    const [patientName, setPatientName] = useState("");
    const [phone, setPhone] = useState("");
    const [date, setDate] = useState("");
    const [time, setTime] = useState("");
    const [msg, setMsg] = useState("");
    const [errors, setErrors] = useState<Record<string, string>>({});

    function validate() {
        const e: Record<string, string> = {};
        if (patientName.trim().length < 2) e.patientName = "Minim 2 caractere.";
        if (!/^\+?\d[\d\s-]{7,}$/.test(phone.trim())) e.phone = "Telefon invalid.";
        if (!date) e.date = "Alege o dată.";
        if (!time) e.time = "Alege o oră.";
        return e;
    }

    function onSubmit(ev: React.FormEvent) {
        ev.preventDefault();
        setMsg("");
        const e = validate();
        setErrors(e);
        if (Object.keys(e).length) return;

        setMsg("Programare creată (mock).");
    }

    return (
        <div style={{ display: "grid", gap: 12 }}>
            <h1 style={{ fontSize: 20, fontWeight: 900 }}>Programare</h1>

            <form onSubmit={onSubmit} style={{ display: "grid", gap: 10, maxWidth: 520 }}>
                <label style={{ display: "grid", gap: 6 }}>
                    <span style={{ fontSize: 13, fontWeight: 800 }}>Nume pacient</span>
                    <input
                        value={patientName}
                        onChange={(e) => setPatientName(e.target.value)}
                        style={{ border: "1px solid #e2e8f0", borderRadius: 10, padding: 10 }}
                    />
                    {errors.patientName && <span style={{ fontSize: 12, color: "#dc2626" }}>{errors.patientName}</span>}
                </label>

                <label style={{ display: "grid", gap: 6 }}>
                    <span style={{ fontSize: 13, fontWeight: 800 }}>Telefon</span>
                    <input value={phone} onChange={(e) => setPhone(e.target.value)} style={{ border: "1px solid #e2e8f0", borderRadius: 10, padding: 10 }} />
                    {errors.phone && <span style={{ fontSize: 12, color: "#dc2626" }}>{errors.phone}</span>}
                </label>

                <div style={{ display: "grid", gap: 10, gridTemplateColumns: "repeat(auto-fit, minmax(160px, 1fr))" }}>
                    <label style={{ display: "grid", gap: 6 }}>
                        <span style={{ fontSize: 13, fontWeight: 800 }}>Data</span>
                        <input type="date" value={date} onChange={(e) => setDate(e.target.value)} style={{ border: "1px solid #e2e8f0", borderRadius: 10, padding: 10 }} />
                        {errors.date && <span style={{ fontSize: 12, color: "#dc2626" }}>{errors.date}</span>}
                    </label>

                    <label style={{ display: "grid", gap: 6 }}>
                        <span style={{ fontSize: 13, fontWeight: 800 }}>Ora</span>
                        <input type="time" value={time} onChange={(e) => setTime(e.target.value)} style={{ border: "1px solid #e2e8f0", borderRadius: 10, padding: 10 }} />
                        {errors.time && <span style={{ fontSize: 12, color: "#dc2626" }}>{errors.time}</span>}
                    </label>
                </div>

                <button type="submit" style={{ background: "black", color: "white", borderRadius: 10, padding: "10px 12px", fontSize: 13, fontWeight: 900 }}>
                    Trimite
                </button>

                {msg && <div style={{ border: "1px solid #bbf7d0", background: "#f0fdf4", padding: 10, borderRadius: 10, fontSize: 13 }}>{msg}</div>}
            </form>
        </div>
    );
}
