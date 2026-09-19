interface AlertProps {
  kind?: "error" | "success";
  message: string | null;
}

export const Alert = ({ kind = "error", message }: AlertProps) => {
  if (!message) {
    return null;
  }

  return <div className={`alert ${kind}`}>{message}</div>;
};
