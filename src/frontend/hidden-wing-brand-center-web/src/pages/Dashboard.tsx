import { useEffect, useState } from "react";
import { dashboardApi } from "../api/dashboardApi";
import { Alert } from "../components/common/Alert";
import { PageHeader } from "../components/common/PageHeader";
import { ApiError, type DashboardSummary } from "../types/api";

export const Dashboard = () => {
  const [summary, setSummary] = useState<DashboardSummary | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    dashboardApi
      .summary()
      .then((response) => setSummary(response.data ?? null))
      .catch((caught: unknown) => {
        setError(caught instanceof ApiError ? caught.message : "Unable to load dashboard.");
      });
  }, []);

  return (
    <div>
      <PageHeader title="Dashboard" description="Active Hidden Wing branding inventory and recent changes." />
      <Alert message={error} />
      <div className="grid-4">
        <article className="card">
          <span className="muted">Active products</span>
          <p className="stat-value">{summary?.activeProducts ?? "—"}</p>
        </article>
        <article className="card">
          <span className="muted">Active brands</span>
          <p className="stat-value">{summary?.activeBrands ?? "—"}</p>
        </article>
        <article className="card">
          <span className="muted">Themes</span>
          <p className="stat-value">{summary?.themes ?? "—"}</p>
        </article>
        <article className="card">
          <span className="muted">Assets</span>
          <p className="stat-value">{summary?.assets ?? "—"}</p>
        </article>
      </div>
      <div className="panel" style={{ marginTop: 20 }}>
        <h2>Recent changes</h2>
        {summary?.recentChanges.length ? (
          <ul>
            {summary.recentChanges.map((change) => (
              <li key={`${change.entityType}-${change.name}-${change.updatedAt}`}>
                <strong>{change.entityType}</strong> · {change.name} · {new Date(change.updatedAt).toLocaleString()}
              </li>
            ))}
          </ul>
        ) : (
          <p className="empty">No recent activity yet.</p>
        )}
      </div>
    </div>
  );
};
