FROM node:20-alpine AS build
WORKDIR /app
COPY src/frontend/hidden-wing-brand-center-web/package.json src/frontend/hidden-wing-brand-center-web/package-lock.json* ./
RUN npm install
COPY src/frontend/hidden-wing-brand-center-web/ ./
ARG VITE_API_BASE_URL=http://localhost:5080
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL
RUN npm run build

FROM nginx:1.27-alpine
COPY deploy/docker/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
