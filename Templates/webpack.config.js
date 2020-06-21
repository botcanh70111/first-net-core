const MiniCssExtractPlugin = require('mini-css-extract-plugin')
const path = require("path");
const isDevelopment = process.env.NODE_ENV === 'development'
const HtmlwebpackPlugin = require('html-webpack-plugin');
const postcssPresetEnv = require('postcss-preset-env');

module.exports = [{
  mode: process.env.NODE_ENV === 'production' ? 'production' : 'development',
  entry: ["./js/index.js", "./css/main.scss"],
  output: {
    filename: 'app.min.js',
    path: path.resolve(__dirname, "./dist"),
    publicPath: "/dist/"
  },
  watch: true,
  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /node_module/,
        loader: 'babel-loader',
        query: {
          cacheDirectory: true,
          presets: ["@babel/preset-env"]
        }
      },
      {
        test: /\.scss$/,
        use: [
          {
            loader: MiniCssExtractPlugin.loader
          },
          {
            // Interprets CSS
            loader: 'css-loader',
            options: {
              importLoaders: 2
            }
          },
          {
            // minify CSS và thêm autoprefix
            loader: 'postcss-loader',
            options: {
              ident: 'postcss',

              // Đặt chế độ tối ưu
              plugins: isDevelopment
                ? () => []
                : () => [
                  postcssPresetEnv({
                    browsers: ['>1%']
                  }),
                  require('cssnano')()
                ]
            }
          },
          {
            loader: 'sass-loader'
          }
        ]
      }
    ]
  },
  plugins: [
    new MiniCssExtractPlugin({
      filename: '[name].css',
      chunkFilename: '[id].css'
    }),
    new HtmlwebpackPlugin({
      template: './home.html'
    })
  ],
  devServer: {
    contentBase: [path.join(__dirname)],
    compress: true,
    port: 8080,
    watchContentBase: true
  }
},
{
  entry: ["./admin/js/index.js", "./admin/css/main.scss"],
  output: {
    filename: 'adminApp.min.js',
    path: path.resolve(__dirname, "./dist/admin")
  },
  watch: true,
  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /node_module/,
        loader: 'babel-loader',
        query: {
          cacheDirectory: true,
          presets: ["@babel/preset-env"]
        }
      },
      {
        test: /\.scss$/,
        use: [
          {
            loader: MiniCssExtractPlugin.loader
          },
          {
            // Interprets CSS
            loader: 'css-loader',
            options: {
              importLoaders: 2
            }
          },
          {
            // minify CSS và thêm autoprefix
            loader: 'postcss-loader',
            options: {
              ident: 'postcss',

              // Đặt chế độ tối ưu
              plugins: isDevelopment
                ? () => []
                : () => [
                  postcssPresetEnv({
                    browsers: ['>1%']
                  }),
                  require('cssnano')()
                ]
            }
          },
          {
            loader: 'sass-loader'
          }
        ]
      }
    ]
  },
  plugins: [
    new MiniCssExtractPlugin({
      filename: '[name].css',
      chunkFilename: '[id].css'
    }),
    new HtmlwebpackPlugin({
      template: './admin/home.html'
    })
  ],
  devServer: {
    contentBase: [path.join(__dirname, "admin")],
    compress: true,
    port: 8080,
    watchContentBase: true
  }
}]