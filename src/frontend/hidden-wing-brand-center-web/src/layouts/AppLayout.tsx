import { NavLink, Outlet } from "react-router-dom";
import { appConfig } from "../config/app.config";
import { tokenStore } from "../api/client";

export const AppLayout = () => {
  return (
    <div className="app-shell">
      <aside className="sidebar">
        <div className="brand-mark">
          <img src="/assets/hidden-wing/icon.svg" alt="Hidden Wing" />
          <div>
            <strong>HIDDEN WING</strong>
            <span>Brand Center</span>
          </div>
        </div>
        <nav className="nav-list">
          {appConfig.navigation.map((item) => (
            <NavLink key={item.path} to={item.path} className={({ isActive }) => (isActive ? "nav-link active" : "nav-link")} end={item.path === "/"}>
              {item.label}
            </NavLink>
          ))}
        </nav>
        <div style={{ marginTop: 32 }}>
          <button
            className="button-secondary"
            onClick={() => {
              tokenStore.clear();
              window.location.href = "/login";
            }}
          >
            Sign out
          </button>
        </div>
      </aside>
      <main className="content">
        <Outlet />
      </main>
    </div>
  );
};
