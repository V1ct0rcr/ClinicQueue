import type { AxiosInstance } from "axios";

export type Role = "User" | "Admin";

export interface AuthUser {
  username: string;
  role: Role;
  userId: string;
}

const TOKEN_KEY = "clinicqueue_token";

function decodePayload(token: string): Record<string, unknown> {
  try {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const json = decodeURIComponent(
      atob(base64)
        .split("")
        .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
        .join("")
    );
    return JSON.parse(json);
  } catch {
    return {};
  }
}

export function getToken(): string | null {
  return localStorage.getItem(TOKEN_KEY);
}

export function getUser(): AuthUser | null {
  const token = getToken();
  if (!token) return null;

  const payload = decodePayload(token);

  const exp = payload["exp"] as number;
  if (exp && Date.now() / 1000 > exp) {
    logout();
    return null;
  }

  const username = (payload["unique_name"] as string) ?? "";
  const role = (payload["role"] as Role) ?? "User";
  const userId = (payload["nameid"] as string) ?? "";

  return { username, role, userId };
}

export function hasRole(requiredRole: Role): boolean {
  const user = getUser();
  if (!user) return false;
  if (user.role === "Admin") return true;
  return user.role === requiredRole;
}

export function isAuthenticated(): boolean {
  return getUser() !== null;
}

export async function login(
  api: AxiosInstance,
  credentialType: string,
  password: string
): Promise<void> {
  const response = await api.post<{ token: string }>("/session/auth", {
    credentialType,
    password,
  });
  localStorage.setItem(TOKEN_KEY, response.data.token);
  window.dispatchEvent(new Event("authchange"));
}

export function logout(): void {
  localStorage.removeItem(TOKEN_KEY);
  window.dispatchEvent(new Event("authchange"));
}
