import { FormEvent, useState } from "react";
import { useNavigate } from "react-router-dom";
import { authApi } from "../api/dashboardApi";
import { tokenStore } from "../api/client";
import { Alert } from "../components/common/Alert";
import { ApiError } from "../types/api";

export const Login = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState("admin");
  const [password, setPassword] = useState("admin");
  const [error, setError] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);

  const onSubmit = async (event: FormEvent) => {
    event.preventDefault();
    setSubmitting(true);
    setError(null);
    try {
      const response = await authApi.login(username, password);
      if (!response.data?.accessToken) {
        throw new Error("Login did not return a token.");
      }
      tokenStore.set(response.data.accessToken);
      navigate("/");
    } catch (caught) {
      setError(caught instanceof ApiError ? caught.message : "Unable to sign in.");
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="login-page">
      <form className="login-card card stack" onSubmit={(event) => void onSubmit(event)}>
        <div className="brand-mark">
          <img src="/assets/hidden-wing/icon.svg" alt="" />
          <div>
            <strong>HIDDEN WING</strong>
            <span>Brand Center</span>
          </div>
        </div>
        <Alert message={error} />
        <label>
          Username
          <input value={username} onChange={(event) => setUsername(event.target.value)} />
        </label>
        <label>
          Password
          <input type="password" value={password} onChange={(event) => setPassword(event.target.value)} />
        </label>
        <button className="button" type="submit" disabled={submitting}>
          {submitting ? "Signing in..." : "Sign in"}
        </button>
      </form>
    </div>
  );
};
