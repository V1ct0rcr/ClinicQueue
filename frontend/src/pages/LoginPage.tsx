import React, { useRef, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { login } from "../services/auth";
import { useApi } from "../context/ApiContext";

export function LoginPage() {
  const api = useApi();
  const navigate = useNavigate();
  const location = useLocation() as { state?: { from?: string } };

  const credentialRef = useRef<HTMLInputElement>(null);
  const passwordRef = useRef<HTMLInputElement>(null);

  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  async function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setError("");
    setLoading(true);

    const credential = credentialRef.current?.value ?? "";
    const password = passwordRef.current?.value ?? "";

    try {
      await login(api, credential, password);
      const backTo = location.state?.from ?? "/";
      navigate(backTo, { replace: true });
    } catch {
      setError("Credențiale incorecte. Verifică username/email și parola.");
    } finally {
      setLoading(false);
    }
  }

  const inputStyle: React.CSSProperties = {
    padding: "10px 14px",
    borderRadius: 10,
    border: "1px solid #e2e8f0",
    fontSize: 15,
    width: "100%",
    boxSizing: "border-box",
  };

  return (
    <div style={{ display: "grid", gap: 16, maxWidth: 400 }}>
      <h1 style={{ margin: 0, fontSize: 28, fontWeight: 900 }}>Login</h1>
      <p style={{ margin: 0, color: "#475569", fontSize: 14 }}>
        Introdu username sau email și parola contului tău.
      </p>

      <form onSubmit={onSubmit} style={{ display: "grid", gap: 12 }}>
        <div style={{ display: "grid", gap: 6 }}>
          <label style={{ fontWeight: 700, fontSize: 14 }}>
            Username sau Email
          </label>
          <input
            ref={credentialRef}
            type="text"
            placeholder="ion.popescu sau ion@email.com"
            required
            style={inputStyle}
          />
        </div>

        <div style={{ display: "grid", gap: 6 }}>
          <label style={{ fontWeight: 700, fontSize: 14 }}>Parolă</label>
          <input
            ref={passwordRef}
            type="password"
            placeholder="••••••••"
            required
            style={inputStyle}
          />
        </div>

        {error && (
          <div
            style={{
              padding: "10px 14px",
              borderRadius: 10,
              background: "#fef2f2",
              border: "1px solid #fecaca",
              color: "#dc2626",
              fontSize: 14,
            }}
          >
            {error}
          </div>
        )}

        <button
          type="submit"
          disabled={loading}
          style={{
            padding: "12px 14px",
            borderRadius: 12,
            border: 0,
            background: loading ? "#94a3b8" : "#0f172a",
            color: "white",
            fontWeight: 900,
            fontSize: 15,
            cursor: loading ? "not-allowed" : "pointer",
          }}
        >
          {loading ? "Se autentifică..." : "Intră în cont"}
        </button>
      </form>
    </div>
  );
}
