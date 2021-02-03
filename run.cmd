docker build --rm --pull -f "Dockerfile" -t "rahulrai/sidecar-monitor-app:latest" .
docker image push rahulrai/sidecar-monitor-app:latest
docker build --rm --pull -f "Dockerfile.tools" -t "rahulrai/sidecar-monitor:latest" .
docker image push rahulrai/sidecar-monitor:latest