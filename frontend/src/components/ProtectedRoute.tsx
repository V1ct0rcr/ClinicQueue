import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import type { Role } from "../services/auth";
import { hasRole } from "../services/auth";

type Props = {
  requiredRole: Role;
  children: React.ReactNode;
};

export function ProtectedRoute({ requiredRole, children }: Props) {
  const location = useLocation();

  if (!hasRole(requiredRole)) {
    return (
      <Navigate to="/login" replace state={{ from: location.pathname }} />
    );
  }

  return <>{children}</>;
}
