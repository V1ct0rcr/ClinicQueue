export function ServerDownPage() {
  return (
    <div
      style={{
        display: "grid",
        placeItems: "center",
        minHeight: "100vh",
        background: "#fef2f2",
        textAlign: "center",
        padding: 24,
      }}
    >
      <div style={{ display: "grid", gap: 12 }}>
        <div style={{ fontSize: 64 }}>⚠️</div>
        <h1 style={{ margin: 0, fontSize: 28, color: "#dc2626" }}>
          Server indisponibil
        </h1>
        <p style={{ margin: 0, color: "#6b7280", maxWidth: 400 }}>
          Nu s-a putut stabili conexiunea cu serverul. Verifică că
          backend-ul este pornit și încearcă din nou.
        </p>
        <button
          onClick={() => window.location.replace("/")}
          style={{
            marginTop: 8,
            padding: "10px 20px",
            borderRadius: 10,
            border: 0,
            background: "#dc2626",
            color: "white",
            fontWeight: 700,
            cursor: "pointer",
            width: "fit-content",
            justifySelf: "center",
          }}
        >
          Încearcă din nou
        </button>
      </div>
    </div>
  );
}
