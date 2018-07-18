/**
 * Copyright (c) 2017-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */

const React = require('react');

class Footer extends React.Component {
  docUrl(doc, language) {
    const baseUrl = this.props.config.baseUrl;
    return baseUrl + 'docs/' + (language ? language + '/' : '') + doc;
  }

  pageUrl(doc, language) {
    const baseUrl = this.props.config.baseUrl;
    return baseUrl + (language ? language + '/' : '') + doc;
  }

  render() {
    const currentYear = new Date().getFullYear();
    return (
      <footer className="nav-footer" id="footer">
        <section className="sitemap">
          <a href={this.props.config.baseUrl} className="nav-home">
            {this.props.config.footerIcon && (
              <img
                src={this.props.config.baseUrl + this.props.config.footerIcon}
                alt={this.props.config.title}
                width="66"
                height="58"
              />
            )}
          </a>
          <div>
            <h5>Docs</h5>
            <a href={this.docUrl('docs.html', this.props.language)}>
              Getting Started
            </a>
            <a href={this.docUrl('basic_guide.html', this.props.language)}>
              Guides
            </a>
          </div>
          <div>
            <h5>Community</h5>
            <a target="_blank" href="https://discordapp.com/invite/b8QNXwD">Discord</a>
            <a target="_blank" href="https://www.reddit.com/r/NEO/">Reddit</a>
            <a target="_blank" href="https://www.facebook.com/CityOfZionOfficial">Facebook</a>
            <a target="_blank" href="https://twitter.com/coz_official">Twitter</a>
            <a target="_blank" href="https://medium.com/@cityofzion">Medium</a>
          </div>
          <div>
            <h5>More</h5>
            <a target="_blank" href="https://cityofzion.io/">Site</a>
            <a
              className="github-button"
              href={this.props.config.repoUrl}
              data-icon="octicon-star"
              data-count-href="https://github.com/CityOfZion/neo-sharp"
              data-show-count={true}
              data-count-aria-label="# stargazers on GitHub"
              aria-label="Star this project on GitHub">
              Star
            </a>
          </div>
        </section>
      </footer>
    );
  }
}

module.exports = Footer;
