export type Role = "guest" | "user" | "admin";

export type AuthUser = {
    username: string;
    role: Role;
};

const KEY = "clinicqueue_user";

export function getUser(): AuthUser | null {
    const raw = localStorage.getItem(KEY);
    if (!raw) return null;
    try {
        return JSON.parse(raw) as AuthUser;
    } catch {
        return null;
    }
}

export function hasRole(requiredRole: Role): boolean {
  const user = getUser();
  if (!user) return false;
  if (user.role === "admin") return true;
  return user.role === requiredRole;
}

export function setUser(user: AuthUser) {
    localStorage.setItem(KEY, JSON.stringify(user));
    window.dispatchEvent(new Event("authchange")); // ✅ anunță UI-ul
}

export function logout() {
    localStorage.removeItem(KEY);
    window.dispatchEvent(new Event("authchange")); // ✅ anunță UI-ul
}
