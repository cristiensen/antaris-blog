# ===== BUILD STAGE =====
FROM node:22-alpine AS build
WORKDIR /app

# Copy package files and install deps
COPY package*.json ./
RUN npm install

# Copy the rest of the app
COPY . .

# Build Angular app for production
RUN npm run build -- --configuration production

# ===== RUNTIME STAGE =====
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

# Remove default nginx static assets
RUN rm -rf ./*

# Copy built Angular app (update path if your dist folder name differs)
COPY --from=build /app/dist/antaris-blog-client/browser ./

# Copy a basic nginx config (optional, but nice for SPA routing)
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
