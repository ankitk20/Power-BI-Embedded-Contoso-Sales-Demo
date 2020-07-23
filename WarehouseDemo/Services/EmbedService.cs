﻿// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

namespace WarehouseDemo.Service
{
	using Microsoft.PowerBI.Api;
	using Microsoft.PowerBI.Api.Models;
	using Microsoft.Rest;
	using Newtonsoft.Json.Linq;
	using System;

	public class EmbedService
	{
		private TokenCredentials tokenCredentials;

		public EmbedService(string aadToken)
		{
			tokenCredentials = new TokenCredentials(aadToken, "Bearer");
		}

		/// <summary>
		/// Generate Embed token and Embed URL
		/// </summary>
		/// <returns></returns>
		public JObject GenerateEmbedParams(Guid workspaceId, Guid reportId)
		{
			// TODO: Pass effective identity once roles are implemented in the report

			using (var pbiClient = new PowerBIClient(new Uri(Constant.PowerBiApiUri), tokenCredentials))
			{
				// Get report info
				var pbiReport = pbiClient.Reports.GetReportInGroup(workspaceId, reportId);

				// Create list of datasets
				var datasets = new GenerateTokenRequestV2Dataset[] { new GenerateTokenRequestV2Dataset(pbiReport.DatasetId) };

				// Create list of reports
				var reports = new GenerateTokenRequestV2Report[] { new GenerateTokenRequestV2Report(reportId) };

				// Create list of workspaces
				var workspaces = new GenerateTokenRequestV2TargetWorkspace[] { new GenerateTokenRequestV2TargetWorkspace(workspaceId) };

				// Create a request for getting Embed token 
				var tokenRequest = new GenerateTokenRequestV2(datasets: datasets, reports: reports, targetWorkspaces: workspaces);

				// Get Embed token
				var embedToken = pbiClient.EmbedToken.GenerateToken(tokenRequest);

				// Capture embed parameters
				var embedParams = new JObject
				{
					{ "Id", pbiReport.Id.ToString() },
					{ "EmbedUrl", pbiReport.EmbedUrl },
					{ "Type", "report" },
					{ "EmbedToken", new JObject {
							{ "Token", embedToken.Token },
							{ "TokenId", embedToken.TokenId },
							{ "Expiration", embedToken.Expiration.ToString() }
						}
					},
					{ "MinutesToExpiration",(int)embedToken.Expiration.Subtract(DateTime.UtcNow).TotalMinutes },
					{ "DefaultPage", null },
					{ "MobileDefaultPage", null }
				};

				return embedParams;
			}
		}
	}
}