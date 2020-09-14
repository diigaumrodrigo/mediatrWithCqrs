FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu

ENV ACCEPT_EULA Y

ARG SA_PASSWORD
ENV SA_PASSWORD ${SA_PASSWORD}

ARG SERVER_HOST
ENV SERVER_HOST ${SERVER_HOST}

ARG USER
ENV USER ${USER}

ARG DATABASE
ENV DATABASE ${DATABASE}

ENV MSSQL_PID Express

RUN mkdir -p /opt/vendas/logs
RUN mkdir -p /opt/vendas/seed-scripts

VOLUME /opt/vendas/logs
VOLUME /opt/vendas/seed-scripts

RUN echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
RUN echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc

WORKDIR /scripts
COPY ./scripts/ ./

RUN chmod +x initialize.sh

RUN ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && ./initialize.sh

CMD [ "/opt/mssql/bin/sqlservr" ]
