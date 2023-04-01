# Semantic Search Using ChatGPT
This repository has all the code and documentation for an example of a Semantic Search Engine Using Chat GPT for a fictituous Tax Administration for the fictitiuous country of Laurania.  

Laurania is a fictional country created for the purpose of examples and case studies in various fields, such as tax administration, economics, legal systems, and public administration. It serves as a neutral and imaginary territory, allowing professionals and educators to develop and discuss hypothetical scenarios without referring to any specific real-world country or jurisdiction. In the context of tax administration, Laurania may have its own tax laws, regulations, and policies that can be used to illustrate concepts or explore issues related to taxation, compliance, enforcement, and international tax treaties. By using Laurania as an example, professionals can avoid potential biases and focus on the principles and best practices that can be applied universally. Please note that any details about Laurania's geography, political structure, or economic system are entirely fictitious and are created for educational purposes only.

The code that is published here runs on .NET Core.  I used Visual Studio Community 2022 and SQL Server Express to build the code base.  

Inside the solution are three projects:  ChatGPTInterface handles all the interaction with ChatGPT, KnowledgeBaseManager is a Windows Forms appication that is used to pupulate the knowledge base, SemanticSearch is a Windows Forms application that is used to query ChatGPT with questions about our knowledgbase.

To build the Database, open the solution, adjust the connection string in the appsettings.json configuration strings, then make sure that the ChatGPTInterface project is set as the Default project.  Then open the PackageManagerConsole and type update-database.  Then, run the system.
