import React, { createContext, useContext, useMemo } from "react";
import axios, { type AxiosInstance } from "axios";

const ApiContext = createContext<AxiosInstance | null>(null);

export function ApiProvider({ children }: { children: React.ReactNode }) {
  const api = useMemo(() => {
    const instance = axios.create({
      baseURL: import.meta.env.VITE_API_URL,
      headers: {
        "Content-Type": "application/json",
      },
    });

    instance.interceptors.request.use((config) => {
      const token = localStorage.getItem("clinicqueue_token");
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    });

    instance.interceptors.response.use(
      (response) => response,
      (error) => {
        if (
          !error.response &&
          !window.location.pathname.startsWith("/500")
        ) {
          window.location.href = "/500";
        }
        return Promise.reject(error);
      }
    );

    return instance;
  }, []);

  return <ApiContext.Provider value={api}>{children}</ApiContext.Provider>;
}

export function useApi(): AxiosInstance {
  const api = useContext(ApiContext);
  if (!api) {
    throw new Error("useApi trebuie folosit în interiorul <ApiProvider>");
  }
  return api;
}
