import { environment } from "../config/environment";
import { PageHeader } from "../components/common/PageHeader";

export const Settings = () => {
  return (
    <div>
      <PageHeader title="Settings" description="Environment and integration defaults for this admin workspace." />
      <div className="panel stack">
        <p>
          <strong>Application:</strong> {environment.appName}
        </p>
        <p>
          <strong>API base URL:</strong> {environment.apiBaseUrl}
        </p>
        <p className="muted">
          Authentication is a lightweight internal JWT intended for later replacement by Hidden Wing HQ identity.
          Database-managed branding remains the source of truth.
        </p>
      </div>
    </div>
  );
};
