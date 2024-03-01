import { dispatch, toQueryString } from '@/utils';
import React from 'react';
import { IServerHistoryItemFilter, IServerItemFilter } from './interfaces';

/**
 * Provides a simple way to manage all the API endpoints.
 * Signs user out if their token expires.
 * @returns API endpoint functions.
 */
export const useApiServerItems = () => {
  return React.useMemo(
    () => ({
      find: async (filter: IServerItemFilter | undefined = {}): Promise<Response> => {
        return await dispatch(`/api/dashboard/server-items?${toQueryString(filter)}`);
      },
      findList: async (filter: IServerItemFilter | undefined = {}): Promise<Response> => {
        return await dispatch(`/api/dashboard/server-items/list?${toQueryString(filter)}`);
      },
      get: async (
        serviceNowKey: string,
        includeFileSystemItems: boolean = false,
      ): Promise<Response> => {
        return await dispatch(
          `/api/dashboard/server-items/${serviceNowKey}?${toQueryString({
            includeFileSystemItems,
          })}`,
        );
      },
      history: async (filter: IServerHistoryItemFilter | undefined = {}): Promise<Response> => {
        return await dispatch(`/api/dashboard/server-items/history?${toQueryString(filter)}`, {
          cache: 'force-cache',
        });
      },
    }),
    [],
  );
};
