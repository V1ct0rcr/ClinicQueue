export type Role = "admin" | "user";

export type AuthUser = {
    name: string;
    role: Role;
};
